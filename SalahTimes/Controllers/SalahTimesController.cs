using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Novacode;
using PrayerTimes;
using SalahTimes.Models;


namespace SalahTimes.Controllers
{
    
    public class SalahTimesController : ApiController
    {
        [HttpGet]
        [Route("api/word/{language}/{town}/{year}/{method}/{lat}/{lon}/{alt}")]
        public IHttpActionResult GenerateYearTableInWord(string language, string town,int year,int method,double lat, double lon, int alt)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            using (var document = DocX.Load(System.Web.HttpContext.Current.Request.MapPath("~\\Template\\WordTemplate.docx")))
            {
                var subTitle =
                    document.Paragraphs.Where(p => p.StyleName == "SubTitleStyle" && !string.IsNullOrEmpty(p.Text));
                foreach (var sub in subTitle)
                {
                    sub.ReplaceText(sub.Text, "IZ Västerås");
                }
                var calculator =
                    new PrayerTimes.PrayerTimesCalculator(
                        new PrayerTimes.Types.Coordinates(lat, lon, alt),
                        (PrayerTimes.Types.CalculationMethods)method);

                //var culture = CultureInfo.CreateSpecificCulture("bs");
                //string monthName = new DateTime(2010, 7, 1).ToString("MMMM", culture).ToUpper();

                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleJan"),
                    "JANUAR", document.Tables.FirstOrDefault(t => t.TableCaption == "January"),
                    new DateTime(year, 1, 1), new DateTime(year, 1, DateTime.DaysInMonth(year, 1)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleFeb"),
                    "FEBRUAR", document.Tables.FirstOrDefault(t => t.TableCaption == "February"),
                    new DateTime(year, 2, 1), new DateTime(year, 2, DateTime.DaysInMonth(year, 2)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleMar"), "MART",
                    document.Tables.FirstOrDefault(t => t.TableCaption == "March"), new DateTime(year, 3, 1),
                    new DateTime(year, 3, DateTime.DaysInMonth(year, 3)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleApr"),
                    "APRIL", document.Tables.FirstOrDefault(t => t.TableCaption == "April"),
                    new DateTime(year, 4, 1), new DateTime(year, 4, DateTime.DaysInMonth(year, 4)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleMay"), "MAJ",
                    document.Tables.FirstOrDefault(t => t.TableCaption == "May"), new DateTime(year, 5, 1),
                    new DateTime(year, 5, DateTime.DaysInMonth(year, 5)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleJun"), "JUNI",
                    document.Tables.FirstOrDefault(t => t.TableCaption == "Juni"), new DateTime(year, 6, 1),
                    new DateTime(year, 6, DateTime.DaysInMonth(year, 6)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleJul"), "JULI",
                    document.Tables.FirstOrDefault(t => t.TableCaption == "July"), new DateTime(year, 7, 1),
                    new DateTime(year, 7, DateTime.DaysInMonth(year, 7)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleAug"),
                    "AUGUST", document.Tables.FirstOrDefault(t => t.TableCaption == "August"),
                    new DateTime(year, 8, 1), new DateTime(year, 8, DateTime.DaysInMonth(year, 8)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleSep"),
                    "SEPTEMBER", document.Tables.FirstOrDefault(t => t.TableCaption == "September"),
                    new DateTime(year, 9, 1), new DateTime(year, 9, DateTime.DaysInMonth(year, 9)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleOkt"),
                    "OKTOBAR", document.Tables.FirstOrDefault(t => t.TableCaption == "October"),
                    new DateTime(year, 10, 1), new DateTime(year, 10, DateTime.DaysInMonth(year, 10)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleNov"),
                    "NOVEMBAR", document.Tables.FirstOrDefault(t => t.TableCaption == "November"),
                    new DateTime(year, 11, 1), new DateTime(year, 11, DateTime.DaysInMonth(year, 11)));
                FillMonthData(calculator, document.Paragraphs.FirstOrDefault(p => p.StyleName == "StyleDec"),
                    "DECEMBAR", document.Tables.FirstOrDefault(t => t.TableCaption == "December"),
                    new DateTime(year, 12, 1), new DateTime(year, 12, DateTime.DaysInMonth(year, 12)));

                using (var stream = new MemoryStream())
                {
                    // processing the stream.
                    document.SaveAs(stream);

                    result.Content = new ByteArrayContent(stream.GetBuffer());
                    result.Content.Headers.ContentDisposition =
                        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                        {
                            FileName = $"PrayerTimes{year}.docx"
                        };
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue(
                            "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                }
                return ResponseMessage(result);

            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/currentday/{method}")]
        public IHttpActionResult GetCurrentDayTimes(int method)
        {
            var todaysDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var timezone = TimeZone.CurrentTimeZone.GetUtcOffset(todaysDate.AddDays(1)).Hours;

            var calculator =
                new PrayerTimes.PrayerTimesCalculator(
                    new PrayerTimes.Types.Coordinates(59.598531, 16.512003, 20),
                    (PrayerTimes.Types.CalculationMethods)method);

            var times = calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(todaysDate, "W. Europe Standard Time"));
            return Ok(new Models.SalahTimes
            {
                Date = todaysDate.ToShortDateString(),
                Imsak = times.Imsak,
                Fajr = times.Fajr,
                Sunrise = times.Sunrise,
                Dhur = times.Dhuhr,
                Asr = times.Asr,
                Sunset = times.Sunset,
                Maghrib = times.Maghrib,
                Isha = times.Isha,
                Midnight = times.Midnight
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/currentweek/{method}")]
        public IHttpActionResult GetCurrentWeekTimes(int method)
        {
            return
                GetSalahTimesCollection2(new PrayerTimes.Types.Coordinates(59.598531,16.512003,20),
                    method,
                    DateTime.Now.FirstDayOfWeek(), 
                    DateTime.Now.LastDayOfWeek());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/currentmonth/{method}")]
        public IHttpActionResult GetCurrentMonthTimes(int method)
        {
            return
                GetSalahTimesCollection2(
                    new PrayerTimes.Types.Coordinates(59.598531, 16.512003, 20),
                    method,
                    DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/currentyear/{method}")]
        public IHttpActionResult GetCurrentYearTimes(int method)
        {
            return
                GetSalahTimesCollection2(
                    new PrayerTimes.Types.Coordinates(59.598531, 16.512003, 20),
                    method,
                    new DateTime(DateTime.Now.Year,1,1), new DateTime(DateTime.Now.Year, 12, 31));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="method"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{year}/{method}/{lat}/{lon}/{alt}")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int method, double lat, double lon, int alt)
        {
            if (method < 0 && method > 12)
                return BadRequest("Wrong calculation method");

            var startCalculationDate = new DateTime(year, 1, 1);
            var endCalculationDate = new DateTime(year, 12, 31);

            return GetSalahTimesCollectionForCalculationMethod(new PrayerTimes.Types.Coordinates(lat, lon, alt),
                (PrayerTimes.Types.CalculationMethods) method, startCalculationDate, endCalculationDate);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="method"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{year}/{month}/{method}/{lat}/{lon}/{alt}")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int month,int method, double lat, double lon, int alt)
        {
            if (method < 0 && method > 12)
                return BadRequest("Wrong calculation method");
            if (month < 1 && month > 12)
                return BadRequest("Wrong month");

            var startCalculationDate = new DateTime(year, month, 1);
            var endCalculationDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return GetSalahTimesCollectionForCalculationMethod(new PrayerTimes.Types.Coordinates(lat, lon, alt),
                (PrayerTimes.Types.CalculationMethods)method, startCalculationDate, endCalculationDate);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="method"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{year}/{month}/{day}/{method}/{lat}/{lon}/{alt}")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int month,int day, int method, double lat, double lon, int alt)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");

            if (month < 1 && month > 12)
                return BadRequest("Wrong month");

            //DateTime validDate;
            //DateTime.TryParse($"{year}{month}{day}", out validDate);

            var calculationDate = new DateTime(year, month, day);
            var coordinates = new PrayerTimes.Types.Coordinates(lat, lon, alt);

            var timezoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(calculationDate.AddDays(1)).Hours;

            var calculator = new PrayerTimes.PrayerTimesCalculator(coordinates, (PrayerTimes.Types.CalculationMethods)method);

            var times = calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(calculationDate, "W. Europe Standard Time"));

            return Ok(new Models.SalahTimes
            {
                Date = calculationDate.ToShortDateString(),
                Imsak = times.Imsak,
                Fajr = times.Fajr,
                Sunrise = times.Sunrise,
                Dhur = times.Dhuhr,
                Asr = times.Asr,
                Sunset = times.Sunset,
                Maghrib = times.Maghrib,
                Isha = times.Isha,
                Midnight = times.Midnight
            });
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="options">Mimimum calculationmethod, latitude and logitude</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{year}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year,[FromUri] SalahTimesOptions options)
        {
            var error = CheckForErrors(options);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var startCalculationDate = new DateTime(year, 1, 1);
            var endCalculationDate = new DateTime(year, 12, 31);

            return GetSalahTimesCollection(options, startCalculationDate, endCalculationDate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="options">>Mimimum calculationmethod, latitude and logitude</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{year}/{month}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int month, [FromUri] SalahTimesOptions options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            var error = CheckForErrors(options);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var startCalculationDate = new DateTime(year, month, 1);
            var endCalculationDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return GetSalahTimesCollection(options, startCalculationDate, endCalculationDate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{year}/{month}/{day}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int month, int day,[FromUri] SalahTimesOptions options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            if (day < 1 || day > DateTime.DaysInMonth(year, month))
                return BadRequest("Incorrect day value");

            var error = CheckForErrors(options);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var calculationDate = new DateTime(year, month, day);
            var coordinates = new PrayerTimes.Types.Coordinates(options.Lat, options.Lon, options.Alt);
            var calculationparameters = new PrayerTimes.Types.CalculationParameters((PrayerTimes.Types.CalculationMethods)options.CalculationMethod,
                (PrayerTimes.Types.AsrMethodMadhab)options.AsrMethod,
                (PrayerTimes.Types.HighLatitudeRule)options.HighLatituteMethod,
                (PrayerTimes.Types.MidnightMethod)options.MidnightMethod,
                new PrayerTimes.Types.PrayerTimeAdjustments(options.Imsak, options.Fajr,
                    options.Dhuhr, options.Asr, options.Maghrib, options.Isha));

            var timezoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(calculationDate.AddDays(1)).Hours;

            var calculator = new PrayerTimes.PrayerTimesCalculator(coordinates, calculationparameters);

            var times = calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(calculationDate, "W.Europe Standard Time"));

            return Ok(new Models.SalahTimes
            {
                Date = calculationDate.ToShortDateString(),
                Imsak = times.Imsak,
                Fajr = times.Fajr,
                Sunrise = times.Sunrise,
                Dhur = times.Dhuhr,
                Asr = times.Asr,
                Sunset = times.Sunset,
                Maghrib = times.Maghrib,
                Isha = times.Isha,
                Midnight = times.Midnight
            });
        }
        //[HttpGet]
        //[Route("salahtimes/{year}/{farj:double}/{ishaangle:double}/options")]
        //public IHttpActionResult GetCustomizablePrayerTimesForSpecificDate(int year,double farjAngle, double ishaAngle,
        //    [FromUri] SalahTimesOptions options)
        //{
            
        //    var error = CheckForErrors(options, farjAngle, ishaAngle);

        //    if (!string.IsNullOrWhiteSpace(error))
        //        return BadRequest(error);

        //    var startCalculationDate = new DateTime(year, 1, 1);
        //    var endCalculationDate = new DateTime(year, 12, 31);

        //    return GetSalahTimesCollection(options, startCalculationDate, endCalculationDate);
        //}
        //[HttpGet]
        //[Route("salahtimes/{year}/{month}/{farj:double}/{ishaangle:double}/options")]
        //public IHttpActionResult GetCustomizablePrayerTimesForSpecificDate(int year, int month,double farjAngle,double ishaAngle,
        //            [FromUri] SalahTimesOptions options)
        //{
        //    if (month > 12 || month < 1)
        //        return BadRequest("Incorrect month value");
        //    var error = ConfigureCalculator(options,farjAngle,ishaAngle);

        //    if (!string.IsNullOrWhiteSpace(error))
        //        return BadRequest(error);

        //    var startCalculationDate = new DateTime(year, month, 1);
        //    var endCalculationDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

        //    return GetSalahTimesCollection(options, startCalculationDate, endCalculationDate);
        //}
        //[HttpGet]
        //[Route("salahtimes/{year}/{month}/{day}/{farjangle}/{ishaangle:double}/options")]
        //public IHttpActionResult GetCustomizablePrayerTimesForSpecificDate(int year, int month, int day,double farjAngle,double ishaAngle,
        //    [FromUri] SalahTimesOptions options)
        //{
        //    if (month > 12 || month < 1)
        //        return BadRequest("Incorrect month value");
        //    if (day < 1 || day > DateTime.DaysInMonth(year, month))
        //        return BadRequest("Incorrect day value");

        //    var error = ConfigureCalculator(options,farjAngle,ishaAngle);

        //    if (!string.IsNullOrWhiteSpace(error))
        //        return BadRequest(error);

        //    var calculationDate = new DateTime(year, month, day);

        //    var timezoneOffset = Tz.CurrentTimeZone.GetUtcOffset(calculationDate.AddDays(1)).Hours;

        //    var times = Calculator.getDatePrayerTimes(calculationDate.Year, calculationDate.Month, calculationDate.Day, options.Lat, options.Lon, timezoneOffset, options.Alt);

        //    return Ok(new Models.SalahTimes
        //    {
        //        Date = calculationDate.ToShortDateString(),
        //        Imsak = times[0],
        //        Fajr = times[1],
        //        Sunrise = times[2],
        //        Dhur = times[3],
        //        Asr = times[4],
        //        Sunset = times[5],
        //        Maghrib = times[6],
        //        Isha = times[7],
        //        Midnight = times[8]
        //    });
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="startCalculationDate"></param>
        /// <param name="endCalculationDate"></param>
        /// <returns></returns>
        private IHttpActionResult GetSalahTimesCollection(SalahTimesOptions options, DateTime startCalculationDate,
            DateTime endCalculationDate)
        {
            var coordinates=new PrayerTimes.Types.Coordinates(options.Lat, options.Lon, options.Alt);
            var calculationparameters = new PrayerTimes.Types.CalculationParameters((PrayerTimes.Types.CalculationMethods)options.CalculationMethod,
                (PrayerTimes.Types.AsrMethodMadhab)options.AsrMethod,
                (PrayerTimes.Types.HighLatitudeRule)options.HighLatituteMethod,
                (PrayerTimes.Types.MidnightMethod)options.MidnightMethod,
                new PrayerTimes.Types.PrayerTimeAdjustments(options.Imsak, options.Fajr,
                    options.Dhuhr, options.Asr, options.Maghrib, options.Isha));

            var calculator = new PrayerTimes.PrayerTimesCalculator(coordinates, calculationparameters);
            
            return Ok(this.GetPrayerTimes(startCalculationDate, endCalculationDate, calculator));
        }
        private IHttpActionResult GetSalahTimesCollection2(PrayerTimes.Types.Coordinates coordinates, int method, DateTime startCalculationDate,
            DateTime endCalculationDate)
        {
            if (method < 0 && method > 12)
                return (IHttpActionResult)this.BadRequest("Wrong calculation method");

            var calculator =
            new PrayerTimes.PrayerTimesCalculator(
                coordinates,
                (PrayerTimes.Types.CalculationMethods)method);

            return Ok(this.GetPrayerTimes(startCalculationDate, endCalculationDate, calculator));
        }

        [HttpGet]
        [Route("api/custom/{year}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDateCustomAngles(int year, [FromUri] SalahTimesOptionsForCustomAngles options)
        {
            var error = CheckForErrors(options);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var startCalculationDate = new DateTime(year, 1, 1);
            var endCalculationDate = new DateTime(year, 12, 31);

            return GetSalahTimesCollectionWithAngles(options, startCalculationDate, endCalculationDate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="options">>Mimimum calculationmethod, latitude and logitude</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/custom/{year}/{month}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDateCustomAngles(int year, int month, [FromUri] SalahTimesOptionsForCustomAngles options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            var error = CheckForErrors(options);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var startCalculationDate = new DateTime(year, month, 1);
            var endCalculationDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return GetSalahTimesCollectionWithAngles(options, startCalculationDate, endCalculationDate);
        }
        private IHttpActionResult GetSalahTimesCollectionWithAngles(SalahTimesOptionsForCustomAngles options, DateTime startCalculationDate,
            DateTime endCalculationDate)
        {
            var coordinates = new PrayerTimes.Types.Coordinates(options.Lat, options.Lon, options.Alt);
            var calculationparametersCustomAngles = new PrayerTimes.Types.CalculationParameters(options.FajrAngle,
                options.IshaAngle,
                (PrayerTimes.Types.AsrMethodMadhab)options.AsrMethod,
                (PrayerTimes.Types.HighLatitudeRule)options.HighLatituteMethod,
                (PrayerTimes.Types.MidnightMethod)options.MidnightMethod,
                new PrayerTimes.Types.PrayerTimeAdjustments(options.Imsak, options.Fajr,
                    options.Dhuhr, options.Asr, options.Maghrib, options.Isha));

            var calculator = new PrayerTimes.PrayerTimesCalculator(coordinates, calculationparametersCustomAngles);

            return Ok(this.GetPrayerTimes(startCalculationDate, endCalculationDate, calculator));
        }

        private List<Models.SalahTimes> GetPrayerTimes(DateTime startCalculationDate, DateTime endCalculationDate,
            PrayerTimes.PrayerTimesCalculator calculator)
        {
            var timesCollection = new List<Models.SalahTimes>();

            for (var date = startCalculationDate;
                date <= endCalculationDate;
                date = date.AddDays(1))
            {
                var timeUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date.AddDays(1)).Hours;

                var times = calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(date, "W. Europe Standard Time"));

                timesCollection.Add(new Models.SalahTimes
                {
                    Date = date.ToShortDateString(),
                    Imsak = times.Imsak,
                    Fajr = times.Fajr,
                    Sunrise = times.Sunrise,
                    Dhur = times.Dhuhr,
                    Asr = times.Asr,
                    Sunset = times.Sunset,
                    Maghrib = times.Maghrib,
                    Isha = times.Isha,
                    Midnight = times.Midnight
                });
            }
            return timesCollection;
        }
        private IHttpActionResult GetSalahTimesCollectionForCalculationMethod(PrayerTimes.Types.Coordinates coordinates, PrayerTimes.Types.CalculationMethods method, DateTime startCalculationDate,
            DateTime endCalculationDate)
        {
            var timesCollection = new List<Models.SalahTimes>();

            var calculator = new PrayerTimes.PrayerTimesCalculator(coordinates, method);

            for (var date = startCalculationDate;
                date <= endCalculationDate;
                date = date.AddDays(1))
            {
                var timeUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date.AddDays(1)).Hours;

                var times = calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(date, "W. Europe Standard Time"));

                timesCollection.Add(new Models.SalahTimes
                {
                    Date = date.ToShortDateString(),
                    Imsak = times.Imsak,
                    Fajr = times.Fajr,
                    Sunrise = times.Sunrise,
                    Dhur = times.Dhuhr,
                    Asr = times.Asr,
                    Sunset = times.Sunset,
                    Maghrib = times.Maghrib,
                    Isha = times.Isha,
                    Midnight = times.Midnight
                });
            }
            return Ok(timesCollection);
        }
        private static string CheckForErrors(SalahTimesOptions options, double farjAngle = double.NaN, double ishaAngle = double.NaN)
        {
            if (options.AsrMethod > 1)
                return "Asr Calculation method can be either 0 (Shafi) or 1(Hanafi)";

            if (options.HighLatituteMethod < 0 || options.HighLatituteMethod > 3)
                return "Incorrect value for highlatitute value can be (0,1,2,3)";

            if(options.MidnightMethod!=0 && options.MidnightMethod!=1)
                return "Incorrect value for midnight method value can be (0-Standard or 1-Jafari)";

            if (options.Alt<0)
                return "Alt is bellow zero.";

            if(options.Lat<-90 || options.Lat>90)
                return "Lat value is not defined.";

            if (options.Lon < -180 || options.Lon > 180)
                return "Lon  value is not defined.";

            if(options.CalculationMethod<0 && options.CalculationMethod>12)
                return "Calculation method is not defined.";
            

            return string.Empty;

        }

        private static void FillMonthData(IPrayerTimesCalculator calculator, Paragraph paragraph, string monthName, Table table, DateTime startDateTime, DateTime endDatetime)
        {
            paragraph.ReplaceText(paragraph.Text, $"{monthName} {startDateTime.Year}");

            for (var date = startDateTime;
                   date <= endDatetime;
                   date = date.AddDays(1))
            {
                var row = SetUpTableRow(table);

                var times = calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(date, "W. Europe Standard Time"));

                row.Cells[0].Paragraphs.First().Append(date.Day.ToString());
                row.Cells[4].Paragraphs.First().Append(times.Fajr);
                row.Cells[5].Paragraphs.First().Append(times.Sunrise);
                row.Cells[6].Paragraphs.First().Append(times.Dhuhr);
                row.Cells[7].Paragraphs.First().Append(times.Asr);
                row.Cells[8].Paragraphs.First().Append(times.Maghrib);
                row.Cells[9].Paragraphs.First().Append(times.Isha);
            }

        }

        private static Row SetUpTableRow(Table table)
        {
            var row = table.InsertRow();
            row.Cells[0].Width = 0.8;
            row.Cells[1].Width = 0.8;
            row.Cells[2].Width = 0.8;
            row.Cells[3].Width = 8.8;
            row.Cells[4].Width = 1.31;
            row.Cells[5].Width = 1.31;
            row.Cells[6].Width = 1.31;
            row.Cells[7].Width = 1.31;
            row.Cells[8].Width = 1.31;
            row.Cells[9].Width = 1.31;
            row.Cells[0].Paragraphs.First().Alignment = Alignment.center;
            row.Cells[4].Paragraphs.First().Alignment = Alignment.center;
            row.Cells[5].Paragraphs.First().Alignment = Alignment.center;
            row.Cells[6].Paragraphs.First().Alignment = Alignment.center;
            row.Cells[7].Paragraphs.First().Alignment = Alignment.center;
            return row;
        }

        //private DateTime GetLocalDateTime(double latitude, double longitude, DateTime utcDate)
        //{
        //    var client = new HttpClient();//);
        //    //client.DefaultRequestHeaders.Accept.Clear();
        //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var requestString = String.Format("https://maps.googleapis.com/maps/api/timezone/json?location={0},{1}&timestamp={2}&key={3}", latitude.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),longitude.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture), utcDate.ToTimestamp(),"AIzaSyBsv5XciqUgv20uJ9JXiB8rgfpiBOow6XY");
        //    var response1 = client.GetAsync(requestString);
        //    var response = response1.Result;
        //    return DateTime.Now;
        //    //return utcDate.AddSeconds(response.Data.rawOffset + response.Data.dstOffset);
        //}
    }
    //public class GoogleTimeZone
    //{
    //    public double dstOffset { get; set; }
    //    public double rawOffset { get; set; }
    //    public string status { get; set; }
    //    public string timeZoneId { get; set; }
    //    public string timeZoneName { get; set; }
    //}

    //public static class ExtensionMethods
    //{
    //    public static double ToTimestamp(this DateTime date)
    //    {
    //        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    //        TimeSpan diff = date.ToUniversalTime() - origin;
    //        return Math.Floor(diff.TotalSeconds);
    //    }
    //}
    public static partial class DateTimeExtensions
    {
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
    }
}

/*
 //DaylightName=Dateline Daylight Time
//DisplayName=(UTC-12:00) International Date Line West
//StandardName=Dateline Standard Time
//BaseUtcOffset=-12:00:00
//SupportsDaylightSavingTime=False
Dateline Standard Time,
//DaylightName=UTC-11
//DisplayName=(UTC-11:00) Coordinated Universal Time-11
//StandardName=UTC-11
//BaseUtcOffset=-11:00:00
//SupportsDaylightSavingTime=False
UTC-11,
//DaylightName=Hawaiian Daylight Time
//DisplayName=(UTC-10:00) Hawaii
//StandardName=Hawaiian Standard Time
//BaseUtcOffset=-10:00:00
//SupportsDaylightSavingTime=False
Hawaiian Standard Time,
//DaylightName=Alaskan Daylight Time
//DisplayName=(UTC-09:00) Alaska
//StandardName=Alaskan Standard Time
//BaseUtcOffset=-09:00:00
//SupportsDaylightSavingTime=True
Alaskan Standard Time,
//DaylightName=Pacific Daylight Time (Mexico)
//DisplayName=(UTC-08:00) Baja California
//StandardName=Pacific Standard Time (Mexico)
//BaseUtcOffset=-08:00:00
//SupportsDaylightSavingTime=True
Pacific Standard Time (Mexico),
//DaylightName=Pacific Daylight Time
//DisplayName=(UTC-08:00) Pacific Time (US & Canada)
//StandardName=Pacific Standard Time
//BaseUtcOffset=-08:00:00
//SupportsDaylightSavingTime=True
Pacific Standard Time,
//DaylightName=US Mountain Daylight Time
//DisplayName=(UTC-07:00) Arizona
//StandardName=US Mountain Standard Time
//BaseUtcOffset=-07:00:00
//SupportsDaylightSavingTime=False
US Mountain Standard Time,
//DaylightName=Mountain Daylight Time (Mexico)
//DisplayName=(UTC-07:00) Chihuahua, La Paz, Mazatlan
//StandardName=Mountain Standard Time (Mexico)
//BaseUtcOffset=-07:00:00
//SupportsDaylightSavingTime=True
Mountain Standard Time (Mexico),
//DaylightName=Mountain Daylight Time
//DisplayName=(UTC-07:00) Mountain Time (US & Canada)
//StandardName=Mountain Standard Time
//BaseUtcOffset=-07:00:00
//SupportsDaylightSavingTime=True
Mountain Standard Time,
//DaylightName=Central America Daylight Time
//DisplayName=(UTC-06:00) Central America
//StandardName=Central America Standard Time
//BaseUtcOffset=-06:00:00
//SupportsDaylightSavingTime=False
Central America Standard Time,
//DaylightName=Central Daylight Time
//DisplayName=(UTC-06:00) Central Time (US & Canada)
//StandardName=Central Standard Time
//BaseUtcOffset=-06:00:00
//SupportsDaylightSavingTime=True
Central Standard Time,
//DaylightName=Central Daylight Time (Mexico)
//DisplayName=(UTC-06:00) Guadalajara, Mexico City, Monterrey
//StandardName=Central Standard Time (Mexico)
//BaseUtcOffset=-06:00:00
//SupportsDaylightSavingTime=True
Central Standard Time (Mexico),
//DaylightName=Canada Central Daylight Time
//DisplayName=(UTC-06:00) Saskatchewan
//StandardName=Canada Central Standard Time
//BaseUtcOffset=-06:00:00
//SupportsDaylightSavingTime=False
Canada Central Standard Time,
//DaylightName=SA Pacific Daylight Time
//DisplayName=(UTC-05:00) Bogota, Lima, Quito, Rio Branco
//StandardName=SA Pacific Standard Time
//BaseUtcOffset=-05:00:00
//SupportsDaylightSavingTime=False
SA Pacific Standard Time,
//DaylightName=Eastern Daylight Time (Mexico)
//DisplayName=(UTC-05:00) Chetumal
//StandardName=Eastern Standard Time (Mexico)
//BaseUtcOffset=-05:00:00
//SupportsDaylightSavingTime=False
Eastern Standard Time (Mexico),
//DaylightName=Eastern Daylight Time
//DisplayName=(UTC-05:00) Eastern Time (US & Canada)
//StandardName=Eastern Standard Time
//BaseUtcOffset=-05:00:00
//SupportsDaylightSavingTime=True
Eastern Standard Time,
//DaylightName=US Eastern Daylight Time
//DisplayName=(UTC-05:00) Indiana (East)
//StandardName=US Eastern Standard Time
//BaseUtcOffset=-05:00:00
//SupportsDaylightSavingTime=True
US Eastern Standard Time,
//DaylightName=Venezuela Daylight Time
//DisplayName=(UTC-04:30) Caracas
//StandardName=Venezuela Standard Time
//BaseUtcOffset=-04:30:00
//SupportsDaylightSavingTime=True
Venezuela Standard Time,
//DaylightName=Paraguay Daylight Time
//DisplayName=(UTC-04:00) Asuncion
//StandardName=Paraguay Standard Time
//BaseUtcOffset=-04:00:00
//SupportsDaylightSavingTime=True
Paraguay Standard Time,
//DaylightName=Atlantic Daylight Time
//DisplayName=(UTC-04:00) Atlantic Time (Canada)
//StandardName=Atlantic Standard Time
//BaseUtcOffset=-04:00:00
//SupportsDaylightSavingTime=True
Atlantic Standard Time,
//DaylightName=Central Brazilian Daylight Time
//DisplayName=(UTC-04:00) Cuiaba
//StandardName=Central Brazilian Standard Time
//BaseUtcOffset=-04:00:00
//SupportsDaylightSavingTime=True
Central Brazilian Standard Time,
//DaylightName=SA Western Daylight Time
//DisplayName=(UTC-04:00) Georgetown, La Paz, Manaus, San Juan
//StandardName=SA Western Standard Time
//BaseUtcOffset=-04:00:00
//SupportsDaylightSavingTime=False
SA Western Standard Time,
//DaylightName=Newfoundland Daylight Time
//DisplayName=(UTC-03:30) Newfoundland
//StandardName=Newfoundland Standard Time
//BaseUtcOffset=-03:30:00
//SupportsDaylightSavingTime=True
Newfoundland Standard Time,
//DaylightName=E. South America Daylight Time
//DisplayName=(UTC-03:00) Brasilia
//StandardName=E. South America Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=True
E. South America Standard Time,
//DaylightName=SA Eastern Daylight Time
//DisplayName=(UTC-03:00) Cayenne, Fortaleza
//StandardName=SA Eastern Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=False
SA Eastern Standard Time,
//DaylightName=Argentina Daylight Time
//DisplayName=(UTC-03:00) City of Buenos Aires
//StandardName=Argentina Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=True
Argentina Standard Time,
//DaylightName=Greenland Daylight Time
//DisplayName=(UTC-03:00) Greenland
//StandardName=Greenland Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=True
Greenland Standard Time,
//DaylightName=Montevideo Daylight Time
//DisplayName=(UTC-03:00) Montevideo
//StandardName=Montevideo Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=True
Montevideo Standard Time,
//DaylightName=Bahia Daylight Time
//DisplayName=(UTC-03:00) Salvador
//StandardName=Bahia Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=True
Bahia Standard Time,
//DaylightName=Pacific SA Daylight Time
//DisplayName=(UTC-03:00) Santiago
//StandardName=Pacific SA Standard Time
//BaseUtcOffset=-03:00:00
//SupportsDaylightSavingTime=True
Pacific SA Standard Time,
//DaylightName=UTC-02
//DisplayName=(UTC-02:00) Coordinated Universal Time-02
//StandardName=UTC-02
//BaseUtcOffset=-02:00:00
//SupportsDaylightSavingTime=False
UTC-02,
//DaylightName=Mid-Atlantic Daylight Time
//DisplayName=(UTC-02:00) Mid-Atlantic - Old
//StandardName=Mid-Atlantic Standard Time
//BaseUtcOffset=-02:00:00
//SupportsDaylightSavingTime=True
Mid-Atlantic Standard Time,
//DaylightName=Azores Daylight Time
//DisplayName=(UTC-01:00) Azores
//StandardName=Azores Standard Time
//BaseUtcOffset=-01:00:00
//SupportsDaylightSavingTime=True
Azores Standard Time,
//DaylightName=Cabo Verde Daylight Time
//DisplayName=(UTC-01:00) Cabo Verde Is.
//StandardName=Cabo Verde Standard Time
//BaseUtcOffset=-01:00:00
//SupportsDaylightSavingTime=False
Cape Verde Standard Time,
//DaylightName=Morocco Daylight Time
//DisplayName=(UTC) Casablanca
//StandardName=Morocco Standard Time
//BaseUtcOffset=00:00:00
//SupportsDaylightSavingTime=True
Morocco Standard Time,
//DaylightName=Coordinated Universal Time
//DisplayName=(UTC) Coordinated Universal Time
//StandardName=Coordinated Universal Time
//BaseUtcOffset=00:00:00
//SupportsDaylightSavingTime=False
UTC,
//DaylightName=GMT Daylight Time
//DisplayName=(UTC) Dublin, Edinburgh, Lisbon, London
//StandardName=GMT Standard Time
//BaseUtcOffset=00:00:00
//SupportsDaylightSavingTime=True
GMT Standard Time,
//DaylightName=Greenwich Daylight Time
//DisplayName=(UTC) Monrovia, Reykjavik
//StandardName=Greenwich Standard Time
//BaseUtcOffset=00:00:00
//SupportsDaylightSavingTime=False
Greenwich Standard Time,
//DaylightName=W. Europe Daylight Time
//DisplayName=(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna
//StandardName=W. Europe Standard Time
//BaseUtcOffset=01:00:00
//SupportsDaylightSavingTime=True
W. Europe Standard Time,
//DaylightName=Central Europe Daylight Time
//DisplayName=(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague
//StandardName=Central Europe Standard Time
//BaseUtcOffset=01:00:00
//SupportsDaylightSavingTime=True
Central Europe Standard Time,
//DaylightName=Romance Daylight Time
//DisplayName=(UTC+01:00) Brussels, Copenhagen, Madrid, Paris
//StandardName=Romance Standard Time
//BaseUtcOffset=01:00:00
//SupportsDaylightSavingTime=True
Romance Standard Time,
//DaylightName=Central European Daylight Time
//DisplayName=(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb
//StandardName=Central European Standard Time
//BaseUtcOffset=01:00:00
//SupportsDaylightSavingTime=True
Central European Standard Time,
//DaylightName=W. Central Africa Daylight Time
//DisplayName=(UTC+01:00) West Central Africa
//StandardName=W. Central Africa Standard Time
//BaseUtcOffset=01:00:00
//SupportsDaylightSavingTime=False
W. Central Africa Standard Time,
//DaylightName=Namibia Daylight Time
//DisplayName=(UTC+01:00) Windhoek
//StandardName=Namibia Standard Time
//BaseUtcOffset=01:00:00
//SupportsDaylightSavingTime=True
Namibia Standard Time,
//DaylightName=Jordan Daylight Time
//DisplayName=(UTC+02:00) Amman
//StandardName=Jordan Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Jordan Standard Time,
//DaylightName=GTB Daylight Time
//DisplayName=(UTC+02:00) Athens, Bucharest
//StandardName=GTB Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
GTB Standard Time,
//DaylightName=Middle East Daylight Time
//DisplayName=(UTC+02:00) Beirut
//StandardName=Middle East Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Middle East Standard Time,
//DaylightName=Egypt Daylight Time
//DisplayName=(UTC+02:00) Cairo
//StandardName=Egypt Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Egypt Standard Time,
//DaylightName=Syria Daylight Time
//DisplayName=(UTC+02:00) Damascus
//StandardName=Syria Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Syria Standard Time,
//DaylightName=E. Europe Daylight Time
//DisplayName=(UTC+02:00) E. Europe
//StandardName=E. Europe Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
E. Europe Standard Time,
//DaylightName=South Africa Daylight Time
//DisplayName=(UTC+02:00) Harare, Pretoria
//StandardName=South Africa Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=False
South Africa Standard Time,
//DaylightName=FLE Daylight Time
//DisplayName=(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius
//StandardName=FLE Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
FLE Standard Time,
//DaylightName=Turkey Daylight Time
//DisplayName=(UTC+02:00) Istanbul
//StandardName=Turkey Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Turkey Standard Time,
//DaylightName=Jerusalem Daylight Time
//DisplayName=(UTC+02:00) Jerusalem
//StandardName=Jerusalem Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Israel Standard Time,
//DaylightName=Russia TZ 1 Daylight Time
//DisplayName=(UTC+02:00) Kaliningrad (RTZ 1)
//StandardName=Russia TZ 1 Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Kaliningrad Standard Time,
//DaylightName=Libya Daylight Time
//DisplayName=(UTC+02:00) Tripoli
//StandardName=Libya Standard Time
//BaseUtcOffset=02:00:00
//SupportsDaylightSavingTime=True
Libya Standard Time,
//DaylightName=Arabic Daylight Time
//DisplayName=(UTC+03:00) Baghdad
//StandardName=Arabic Standard Time
//BaseUtcOffset=03:00:00
//SupportsDaylightSavingTime=True
Arabic Standard Time,
//DaylightName=Arab Daylight Time
//DisplayName=(UTC+03:00) Kuwait, Riyadh
//StandardName=Arab Standard Time
//BaseUtcOffset=03:00:00
//SupportsDaylightSavingTime=False
Arab Standard Time,
//DaylightName=Belarus Daylight Time
//DisplayName=(UTC+03:00) Minsk
//StandardName=Belarus Standard Time
//BaseUtcOffset=03:00:00
//SupportsDaylightSavingTime=True
Belarus Standard Time,
//DaylightName=Russia TZ 2 Daylight Time
//DisplayName=(UTC+03:00) Moscow, St. Petersburg, Volgograd (RTZ 2)
//StandardName=Russia TZ 2 Standard Time
//BaseUtcOffset=03:00:00
//SupportsDaylightSavingTime=True
Russian Standard Time,
//DaylightName=E. Africa Daylight Time
//DisplayName=(UTC+03:00) Nairobi
//StandardName=E. Africa Standard Time
//BaseUtcOffset=03:00:00
//SupportsDaylightSavingTime=False
E. Africa Standard Time,
//DaylightName=Iran Daylight Time
//DisplayName=(UTC+03:30) Tehran
//StandardName=Iran Standard Time
//BaseUtcOffset=03:30:00
//SupportsDaylightSavingTime=True
Iran Standard Time,
//DaylightName=Arabian Daylight Time
//DisplayName=(UTC+04:00) Abu Dhabi, Muscat
//StandardName=Arabian Standard Time
//BaseUtcOffset=04:00:00
//SupportsDaylightSavingTime=False
Arabian Standard Time,
//DaylightName=Azerbaijan Daylight Time
//DisplayName=(UTC+04:00) Baku
//StandardName=Azerbaijan Standard Time
//BaseUtcOffset=04:00:00
//SupportsDaylightSavingTime=True
Azerbaijan Standard Time,
//DaylightName=Russia TZ 3 Daylight Time
//DisplayName=(UTC+04:00) Izhevsk, Samara (RTZ 3)
//StandardName=Russia TZ 3 Standard Time
//BaseUtcOffset=04:00:00
//SupportsDaylightSavingTime=False
Russia Time Zone 3,
//DaylightName=Mauritius Daylight Time
//DisplayName=(UTC+04:00) Port Louis
//StandardName=Mauritius Standard Time
//BaseUtcOffset=04:00:00
//SupportsDaylightSavingTime=True
Mauritius Standard Time,
//DaylightName=Georgian Daylight Time
//DisplayName=(UTC+04:00) Tbilisi
//StandardName=Georgian Standard Time
//BaseUtcOffset=04:00:00
//SupportsDaylightSavingTime=False
Georgian Standard Time,
//DaylightName=Caucasus Daylight Time
//DisplayName=(UTC+04:00) Yerevan
//StandardName=Caucasus Standard Time
//BaseUtcOffset=04:00:00
//SupportsDaylightSavingTime=True
Caucasus Standard Time,
//DaylightName=Afghanistan Daylight Time
//DisplayName=(UTC+04:30) Kabul
//StandardName=Afghanistan Standard Time
//BaseUtcOffset=04:30:00
//SupportsDaylightSavingTime=False
Afghanistan Standard Time,
//DaylightName=West Asia Daylight Time
//DisplayName=(UTC+05:00) Ashgabat, Tashkent
//StandardName=West Asia Standard Time
//BaseUtcOffset=05:00:00
//SupportsDaylightSavingTime=False
West Asia Standard Time,
//DaylightName=Russia TZ 4 Daylight Time
//DisplayName=(UTC+05:00) Ekaterinburg (RTZ 4)
//StandardName=Russia TZ 4 Standard Time
//BaseUtcOffset=05:00:00
//SupportsDaylightSavingTime=True
Ekaterinburg Standard Time,
//DaylightName=Pakistan Daylight Time
//DisplayName=(UTC+05:00) Islamabad, Karachi
//StandardName=Pakistan Standard Time
//BaseUtcOffset=05:00:00
//SupportsDaylightSavingTime=True
Pakistan Standard Time,
//DaylightName=India Daylight Time
//DisplayName=(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi
//StandardName=India Standard Time
//BaseUtcOffset=05:30:00
//SupportsDaylightSavingTime=False
India Standard Time,
//DaylightName=Sri Lanka Daylight Time
//DisplayName=(UTC+05:30) Sri Jayawardenepura
//StandardName=Sri Lanka Standard Time
//BaseUtcOffset=05:30:00
//SupportsDaylightSavingTime=False
Sri Lanka Standard Time,
//DaylightName=Nepal Daylight Time
//DisplayName=(UTC+05:45) Kathmandu
//StandardName=Nepal Standard Time
//BaseUtcOffset=05:45:00
//SupportsDaylightSavingTime=False
Nepal Standard Time,
//DaylightName=Central Asia Daylight Time
//DisplayName=(UTC+06:00) Astana
//StandardName=Central Asia Standard Time
//BaseUtcOffset=06:00:00
//SupportsDaylightSavingTime=False
Central Asia Standard Time,
//DaylightName=Bangladesh Daylight Time
//DisplayName=(UTC+06:00) Dhaka
//StandardName=Bangladesh Standard Time
//BaseUtcOffset=06:00:00
//SupportsDaylightSavingTime=True
Bangladesh Standard Time,
//DaylightName=Russia TZ 5 Daylight Time
//DisplayName=(UTC+06:00) Novosibirsk (RTZ 5)
//StandardName=Russia TZ 5 Standard Time
//BaseUtcOffset=06:00:00
//SupportsDaylightSavingTime=True
N. Central Asia Standard Time,
//DaylightName=Myanmar Daylight Time
//DisplayName=(UTC+06:30) Yangon (Rangoon)
//StandardName=Myanmar Standard Time
//BaseUtcOffset=06:30:00
//SupportsDaylightSavingTime=False
Myanmar Standard Time,
//DaylightName=SE Asia Daylight Time
//DisplayName=(UTC+07:00) Bangkok, Hanoi, Jakarta
//StandardName=SE Asia Standard Time
//BaseUtcOffset=07:00:00
//SupportsDaylightSavingTime=False
SE Asia Standard Time,
//DaylightName=Russia TZ 6 Daylight Time
//DisplayName=(UTC+07:00) Krasnoyarsk (RTZ 6)
//StandardName=Russia TZ 6 Standard Time
//BaseUtcOffset=07:00:00
//SupportsDaylightSavingTime=True
North Asia Standard Time,
//DaylightName=China Daylight Time
//DisplayName=(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi
//StandardName=China Standard Time
//BaseUtcOffset=08:00:00
//SupportsDaylightSavingTime=False
China Standard Time,
//DaylightName=Russia TZ 7 Daylight Time
//DisplayName=(UTC+08:00) Irkutsk (RTZ 7)
//StandardName=Russia TZ 7 Standard Time
//BaseUtcOffset=08:00:00
//SupportsDaylightSavingTime=True
North Asia East Standard Time,
//DaylightName=Malay Peninsula Daylight Time
//DisplayName=(UTC+08:00) Kuala Lumpur, Singapore
//StandardName=Malay Peninsula Standard Time
//BaseUtcOffset=08:00:00
//SupportsDaylightSavingTime=False
Singapore Standard Time,
//DaylightName=W. Australia Daylight Time
//DisplayName=(UTC+08:00) Perth
//StandardName=W. Australia Standard Time
//BaseUtcOffset=08:00:00
//SupportsDaylightSavingTime=True
W. Australia Standard Time,
//DaylightName=Taipei Daylight Time
//DisplayName=(UTC+08:00) Taipei
//StandardName=Taipei Standard Time
//BaseUtcOffset=08:00:00
//SupportsDaylightSavingTime=False
Taipei Standard Time,
//DaylightName=Ulaanbaatar Daylight Time
//DisplayName=(UTC+08:00) Ulaanbaatar
//StandardName=Ulaanbaatar Standard Time
//BaseUtcOffset=08:00:00
//SupportsDaylightSavingTime=True
Ulaanbaatar Standard Time,
//DaylightName=North Korea Daylight Time
//DisplayName=(UTC+08:30) Pyongyang
//StandardName=North Korea Standard Time
//BaseUtcOffset=08:30:00
//SupportsDaylightSavingTime=False
North Korea Standard Time,
//DaylightName=Tokyo Daylight Time
//DisplayName=(UTC+09:00) Osaka, Sapporo, Tokyo
//StandardName=Tokyo Standard Time
//BaseUtcOffset=09:00:00
//SupportsDaylightSavingTime=False
Tokyo Standard Time,
//DaylightName=Korea Daylight Time
//DisplayName=(UTC+09:00) Seoul
//StandardName=Korea Standard Time
//BaseUtcOffset=09:00:00
//SupportsDaylightSavingTime=False
Korea Standard Time,
//DaylightName=Russia TZ 8 Daylight Time
//DisplayName=(UTC+09:00) Yakutsk (RTZ 8)
//StandardName=Russia TZ 8 Standard Time
//BaseUtcOffset=09:00:00
//SupportsDaylightSavingTime=True
Yakutsk Standard Time,
//DaylightName=Cen. Australia Daylight Time
//DisplayName=(UTC+09:30) Adelaide
//StandardName=Cen. Australia Standard Time
//BaseUtcOffset=09:30:00
//SupportsDaylightSavingTime=True
Cen. Australia Standard Time,
//DaylightName=AUS Central Daylight Time
//DisplayName=(UTC+09:30) Darwin
//StandardName=AUS Central Standard Time
//BaseUtcOffset=09:30:00
//SupportsDaylightSavingTime=False
AUS Central Standard Time,
//DaylightName=E. Australia Daylight Time
//DisplayName=(UTC+10:00) Brisbane
//StandardName=E. Australia Standard Time
//BaseUtcOffset=10:00:00
//SupportsDaylightSavingTime=False
E. Australia Standard Time,
//DaylightName=AUS Eastern Daylight Time
//DisplayName=(UTC+10:00) Canberra, Melbourne, Sydney
//StandardName=AUS Eastern Standard Time
//BaseUtcOffset=10:00:00
//SupportsDaylightSavingTime=True
AUS Eastern Standard Time,
//DaylightName=West Pacific Daylight Time
//DisplayName=(UTC+10:00) Guam, Port Moresby
//StandardName=West Pacific Standard Time
//BaseUtcOffset=10:00:00
//SupportsDaylightSavingTime=False
West Pacific Standard Time,
//DaylightName=Tasmania Daylight Time
//DisplayName=(UTC+10:00) Hobart
//StandardName=Tasmania Standard Time
//BaseUtcOffset=10:00:00
//SupportsDaylightSavingTime=True
Tasmania Standard Time,
//DaylightName=Magadan Daylight Time
//DisplayName=(UTC+10:00) Magadan
//StandardName=Magadan Standard Time
//BaseUtcOffset=10:00:00
//SupportsDaylightSavingTime=True
Magadan Standard Time,
//DaylightName=Russia TZ 9 Daylight Time
//DisplayName=(UTC+10:00) Vladivostok, Magadan (RTZ 9)
//StandardName=Russia TZ 9 Standard Time
//BaseUtcOffset=10:00:00
//SupportsDaylightSavingTime=True
Vladivostok Standard Time,
//DaylightName=Russia TZ 10 Daylight Time
//DisplayName=(UTC+11:00) Chokurdakh (RTZ 10)
//StandardName=Russia TZ 10 Standard Time
//BaseUtcOffset=11:00:00
//SupportsDaylightSavingTime=False
Russia Time Zone 10,
//DaylightName=Central Pacific Daylight Time
//DisplayName=(UTC+11:00) Solomon Is., New Caledonia
//StandardName=Central Pacific Standard Time
//BaseUtcOffset=11:00:00
//SupportsDaylightSavingTime=False
Central Pacific Standard Time,
//DaylightName=Russia TZ 11 Daylight Time
//DisplayName=(UTC+12:00) Anadyr, Petropavlovsk-Kamchatsky (RTZ 11)
//StandardName=Russia TZ 11 Standard Time
//BaseUtcOffset=12:00:00
//SupportsDaylightSavingTime=False
Russia Time Zone 11,
//DaylightName=New Zealand Daylight Time
//DisplayName=(UTC+12:00) Auckland, Wellington
//StandardName=New Zealand Standard Time
//BaseUtcOffset=12:00:00
//SupportsDaylightSavingTime=True
New Zealand Standard Time,
//DaylightName=UTC+12
//DisplayName=(UTC+12:00) Coordinated Universal Time+12
//StandardName=UTC+12
//BaseUtcOffset=12:00:00
//SupportsDaylightSavingTime=False
UTC+12,
//DaylightName=Fiji Daylight Time
//DisplayName=(UTC+12:00) Fiji
//StandardName=Fiji Standard Time
//BaseUtcOffset=12:00:00
//SupportsDaylightSavingTime=True
Fiji Standard Time,
//DaylightName=Kamchatka Daylight Time
//DisplayName=(UTC+12:00) Petropavlovsk-Kamchatsky - Old
//StandardName=Kamchatka Standard Time
//BaseUtcOffset=12:00:00
//SupportsDaylightSavingTime=True
Kamchatka Standard Time,
//DaylightName=Tonga Daylight Time
//DisplayName=(UTC+13:00) Nuku'alofa
//StandardName=Tonga Standard Time
//BaseUtcOffset=13:00:00
//SupportsDaylightSavingTime=False
Tonga Standard Time,
//DaylightName=Samoa Daylight Time
//DisplayName=(UTC+13:00) Samoa
//StandardName=Samoa Standard Time
//BaseUtcOffset=13:00:00
//SupportsDaylightSavingTime=True
Samoa Standard Time,
//DaylightName=Line Islands Daylight Time
//DisplayName=(UTC+14:00) Kiritimati Island
//StandardName=Line Islands Standard Time
//BaseUtcOffset=14:00:00
//SupportsDaylightSavingTime=False
Line Islands Standard Time,

     
     */
