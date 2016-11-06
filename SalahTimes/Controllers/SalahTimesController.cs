using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using SalahTimes.Domain;
using SalahTimes.Experiments;
using SalahTimes.Models;
using CalculationMethods = SalahTimes.Domain.CalculationMethods;

namespace SalahTimes.Controllers
{
    
    public class SalahTimesController : ApiController
    {
        private PraytimeCalculator _calc = null;

        private PraytimeCalculator Calculator
        {
            get { return _calc ?? (_calc = new PraytimeCalculator()); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/currentday/{method}")]
        public IHttpActionResult GetCurrentDayTimes(int method)
        {
            var todaysDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            todaysDate = new DateTime(2016, 10, 10);
            var timezone = TimeZone.CurrentTimeZone.GetUtcOffset(todaysDate.AddDays(1)).Hours;

            
                //var calc1 = new NewTimesCalc(true);
                //var times2 = calc1.getTimes(new DateComponent(todaysDate, timezone), new Coordinates(59.61, 16.545, 0), CalculationMethods.UMM_AL_QURA);
                var calc1= new NewTimesCalc(true);
                var times2 = calc1.getTimes(new DateComponent(todaysDate, timezone), new Coordinates(59.598546, 16.511995, 0),CalculationMethods.MOON_SIGHTING_COMMITTEE);

            // the code that you want to measure comes here
                var calc = new PrayerTimeCalcBackup(true);
                var times1 = calc.getTimes(todaysDate, 59.598546, 16.511995, timezone, 0);


            //var times3 = new SalahTimes.Experiments.PrayerCalc2();
            //times3.CalculateTimes(new DateComponent(todaysDate,timezone), new Coordinates(59.61, 16.545));



            Calculator.setHighLatsMethod(2);// za svedsku
            Calculator.setMaghribMinutes(3);
            Calculator.setAsrMethod(0);
            Calculator.setDhuhrMinutes(5);

            Calculator.setCalcMethod(method);

            //var times = Calculator.getDatePrayerTimes(todaysDate.Year, todaysDate.Month, todaysDate.Day, 59.598531, 16.512003, timezone, 0);

            var times = Calculator.getDatePrayerTimes(todaysDate.Year, todaysDate.Month, todaysDate.Day, 59.61, 16.545,
                timezone, 0);
            return Ok(new Models.SalahTimes
            {
                Date = todaysDate.ToShortDateString(),
                Imsak = times[0],
                Fajr = times[1],
                Sunrise = times[2],
                Dhur = times[3],
                Asr = times[4],
                Sunset = times[5],
                Maghrib = times[6],
                Isha = times[7],
                Midnight = times[8]
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/currentweek/{method}")]
        public IHttpActionResult GetCurrentWeekTimes(int method)
        {
            //59.598531, 16.512003
            Calculator.setHighLatsMethod(2);// za svedsku
            Calculator.setDhuhrMinutes(5);
            Calculator.setMaghribMinutes(3);
            Calculator.setAsrMethod(0);
            Calculator.setCalcMethod(method);

            return GetSalahTimesCollection(new SalahTimesOptions { Latitude = 59.598531, Longitude = 16.512003, Altitude = 0 },
               DateTime.Now.FirstDayOfWeek(), DateTime.Now.LastDayOfWeek());
        }
        [HttpGet]
        [Route("salahtimes/currentmonth/{method}")]
        public IHttpActionResult GetCurrentMonthTimes(int method)
        {

            Calculator.setHighLatsMethod(2);// za svedsku
            Calculator.setDhuhrMinutes(5);
            Calculator.setMaghribMinutes(3);
            Calculator.setAsrMethod(0);

            Calculator.setCalcMethod(method);

            return GetSalahTimesCollection(new SalahTimesOptions {Latitude = 59.598531, Longitude = 16.512003, Altitude = 0},
                DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/{year}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year,
            [FromUri] SalahTimesOptions options)
        {
            var error = ConfigureCalculator(options);

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
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/{year}/{month}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int month,
                   [FromUri] SalahTimesOptions options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            var error = ConfigureCalculator(options);

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
        [Route("salahtimes/{year}/{month}/{day}/")]
        public IHttpActionResult GetPrayerTimesForSpecificDate(int year, int month, int day,
            [FromUri] SalahTimesOptions options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            if (day < 1 || day > DateTime.DaysInMonth(year, month))
                return BadRequest("Incorrect day value");

            var error = ConfigureCalculator(options);
            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var calculationDate = new DateTime(year, month, day);

            var timezoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(calculationDate.AddDays(1)).Hours;

            var times = Calculator.getDatePrayerTimes(calculationDate.Year, calculationDate.Month, calculationDate.Day, options.Latitude, options.Longitude, timezoneOffset, options.Altitude);

            return Ok(new Models.SalahTimes
            {
                Date = calculationDate.ToShortDateString(),
                Imsak = times[0],
                Fajr = times[1],
                Sunrise = times[2],
                Dhur = times[3],
                Asr = times[4],
                Sunset = times[5],
                Maghrib = times[6],
                Isha = times[7],
                Midnight = times[8]
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="farjAngle"></param>
        /// <param name="ishaAngle"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/{year}/{farj:double}/{ishaangle:double}/options")]
        public IHttpActionResult GetCustomizablePrayerTimesForSpecificDate(int year,double farjAngle, double ishaAngle,
            [FromUri] SalahTimesOptions options)
        {
            var error = ConfigureCalculator(options, farjAngle, ishaAngle);

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
        /// <param name="farjAngle"></param>
        /// <param name="ishaAngle"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/{year}/{month}/{farj:double}/{ishaangle:double}/options")]
        public IHttpActionResult GetCustomizablePrayerTimesForSpecificDate(int year, int month,double farjAngle,double ishaAngle,
                    [FromUri] SalahTimesOptions options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            var error = ConfigureCalculator(options,farjAngle,ishaAngle);

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
       /// <param name="farjAngle"></param>
       /// <param name="ishaAngle"></param>
       /// <param name="options"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("salahtimes/{year}/{month}/{day}/{farjangle}/{ishaangle:double}/options")]
        public IHttpActionResult GetCustomizablePrayerTimesForSpecificDate(int year, int month, int day,double farjAngle,double ishaAngle,
            [FromUri] SalahTimesOptions options)
        {
            if (month > 12 || month < 1)
                return BadRequest("Incorrect month value");
            if (day < 1 || day > DateTime.DaysInMonth(year, month))
                return BadRequest("Incorrect day value");

            var error = ConfigureCalculator(options,farjAngle,ishaAngle);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var calculationDate = new DateTime(year, month, day);

            var timezoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(calculationDate.AddDays(1)).Hours;

            var times = Calculator.getDatePrayerTimes(calculationDate.Year, calculationDate.Month, calculationDate.Day, options.Latitude, options.Longitude, timezoneOffset, options.Altitude);

            return Ok(new Models.SalahTimes
            {
                Date = calculationDate.ToShortDateString(),
                Imsak = times[0],
                Fajr = times[1],
                Sunrise = times[2],
                Dhur = times[3],
                Asr = times[4],
                Sunset = times[5],
                Maghrib = times[6],
                Isha = times[7],
                Midnight = times[8]
            });
        }
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
            var timesCollection = new List<Models.SalahTimes>();
            var calc = new NewTimesCalc(true);
            for (var date = startCalculationDate;
                date <= endCalculationDate;
                date = date.AddDays(1))
            {
                var timezone = TimeZone.CurrentTimeZone.GetUtcOffset(date.AddDays(1)).Hours;
               
                var times = Calculator.getDatePrayerTimes(date.Year, date.Month, date.Day, options.Latitude, options.Longitude,
                    timezone, options.Altitude);

                timesCollection.Add(new Models.SalahTimes
                {
                    Date = date.ToShortDateString(),
                    Imsak = times[0],
                    Fajr = times[1],
                    Sunrise = times[2],
                    Dhur = times[3],
                    Asr = times[4],
                    Sunset = times[5],
                    Maghrib = times[6],
                    Isha = times[7],
                    Midnight = times[8]
                });
            }
            return Ok(timesCollection);
        }

        private string ConfigureCalculator(SalahTimesOptions options, double farjAngle= double.NaN, double ishaAngle= double.NaN)
        {
            if (options.AsrMethod > 1)
                return "Asr Calculation method can be either 0 (Shafi) or 1(Hanafi)";

            if (options.HighLatituteMethod < 0 || options.HighLatituteMethod > 3)
                return "Incorrect value for highlatitute value can be (0,1,2,3)";

            Calculator.setHighLatsMethod(options.HighLatituteMethod);

            if (options.ImsakMinutesBeforeFajr > 0)
                Calculator.setImsakMinutes(options.ImsakMinutesBeforeFajr);
            if (options.DhurMinutesAfterMidDay > 0)
                Calculator.setDhuhrMinutes(options.DhurMinutesAfterMidDay);
            if (options.MaghribMinutesAfterSunset > 0)
                Calculator.setMaghribMinutes(options.MaghribMinutesAfterSunset);
            if (options.IshaMinutesAfterMaghrib > 0)
                Calculator.setIshaMinutes(options.IshaMinutesAfterMaghrib);

            Calculator.setAsrMethod(options.AsrMethod);
            Calculator.setCalcMethod(options.CalculationMethod);

            if (double.IsNaN(farjAngle) || double.IsNaN(ishaAngle))
                return string.Empty;

            Calculator.setFajrAngle(farjAngle);
            Calculator.setIshaAngle(ishaAngle);

            return string.Empty;

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
