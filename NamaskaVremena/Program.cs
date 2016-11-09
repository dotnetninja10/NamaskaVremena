using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace NamaskaVremena
{
    class Program
    {
        //GOOGLE API KEY for Maps: AIzaSyBkW1mGblBxHAtcL4Zkr9RFHqmrI4crqig
        //The 1/7th night rule is one of the several rules available in fiqh for determining Fajr and Isha times.
        //This rule divides the total night between sunset and sunrise into seven parts; Isha starts when the first seventh part ends, 
        //and Fajr starts when the last seventh part starts.
        //For locations in greater latitudes, depending on the Fajr and Isha calculation method being used, 
        //if PrayerMinder determines that in summer days, total darkness will not happen, 
        //then it switches to using 1/7th night rule if the 1/7th night rule would give later Fajr time, //
        //and/or earlier Isha time.This method results in a smooth transition from the twilight angle method to the 1/7th night method.
        //If the Fajr/Isha method specifies a fixed time after Maghrib for Isha, or a fixed time before sunrise for Fajr, then the 1/7th night rule is never applied.
        static void Main(string[] args)
        {
            HijriCalendaretest();
            var test= new PrayTimeNS.PrayTime();

            var p = new PrayTime2.PrayTime();
            double latitude = 59.598546;//Västerås
            double longitude = 16.511995;
            //double latitude = 59.61;

            //double longitude = 16.545;
            //double latitude = 55.60697799032329; //malmö
            //double longitude = 12.998828125000045;

            //double latitude = 57.77817874481053;
            //double longitude = 14.17442353125;//jönköping

            //double latitude = 59.337741827685335;
            //double longitude = 18.067016915039062;//Stockholm

            //double latitude = 59.99795595469116;//Fagersta
            //double longitude = 15.814819649414062;

            //double latitude = 59.56;
            //double longitude = 10.45;
            //double latitude = 43.86423758202495;//SArajevo
            //double longitude = 18.436432198242187;

            //double latitude = 48.16425328839555;//WIEN
            //double longitude = 16.398468331054687;

            //double latitude = 53.56478277382339;//Hamburg
            //double longitude = 10.004425362304687;
            var calc1 = new SalahTimes.Domain.NewTimesCalc(true);
            //var times2 = calc1.getTimes(new DateComponent(todaysDate, 1), new Coordinates(42.17, 12.04, 0), CalculationMethods.MOON_SIGHTING_COMMITTEE);
            int y = 2015, m = 11, d = 12, tz = 1;
            //GetSunSetAndSunRise(new DateTime(2015, 4, 21), 59.598554481500855, 16.513191775039672);


            //using (
            //    StreamWriter writer1 =
            //        new StreamWriter(@"C:\Users\kurarm\Documents\IdM\zones.txt"))
            //{
            //    Console.SetOut(writer1);
            //    var zones = TimeZoneInfo.GetSystemTimeZones();
            //    foreach (var timeZoneInfo in zones)
            //    {
            //        Console.WriteLine("//DaylightName={0}", timeZoneInfo.DaylightName);
            //        Console.WriteLine("//DisplayName={0}", timeZoneInfo.DisplayName);
            //        Console.WriteLine("//StandardName={0}", timeZoneInfo.StandardName);
            //        Console.WriteLine("//BaseUtcOffset={0}", timeZoneInfo.BaseUtcOffset);
            //        Console.WriteLine("//SupportsDaylightSavingTime={0}", timeZoneInfo.SupportsDaylightSavingTime);
            //        Console.WriteLine("{0},", timeZoneInfo.Id);

            //    }
            //}

            var calculator =
                 new PrayerTimes.PrayerTimesCalculator(
                     new PrayerTimes.Types.Coordinates(59.598531, 16.512003, 20),
                     PrayerTimes.Types.CalculationMethods.MOON_SIGHTING_COMMITTEE);

            using (
                StreamWriter writer =
                    new StreamWriter(String.Format(@"C:\Users\kurarm\Documents\IdM\pray{0}.txt", latitude)))
            {
                Console.SetOut(writer);
                Console.WriteLine("Latitude={0}", latitude);
                Console.WriteLine("Longitude={0}", longitude);
                Console.WriteLine(
                    "farj sunrise Dhuhr Asr(H) Maghrib Isha");
                for (DateTime date = new DateTime(2017, 1, 1);
                    date <= new DateTime(2017, 12, 31);
                    date = date.AddDays(1))
                {
                    //var timzoneFixed1 = TimeZone.CurrentTimeZone.GetUtcOffset(date.AddDays(1)).Hours;
                    ////Imsak, Fajr, Sunrise, Dhuhr, Asr, Sunset, Maghrib, Isha

                    //var times2 = calc1.getTimes(new SalahTimes.Domain.DateComponent(date, timzoneFixed1), 
                    //    new SalahTimes.Domain.Coordinates(latitude, longitude, 0), SalahTimes.Domain.CalculationMethods.MOON_SIGHTING_COMMITTEE);
                    //Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", date.ToShortDateString(), times2[0],times2[1], times2[2], times2[3], times2[4], times2[6], times2[7],timzoneFixed1);



                    //var tim=calculator.GetPrayerTimesForDate(new PrayerTimes.Types.DateComponent(date, "W. Europe Standard Time"));
                    //Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", date.ToShortDateString(),tim.Imsak, tim.Fajr,tim.Sunrise,tim.Dhuhr,tim.Asr,tim.Maghrib,tim.Isha);

                    var result = CustomCalc(date.Year, date.Month, date.Day, test, latitude, longitude);
                    Console.WriteLine("{6} {0} {1} {2} {3} {4} {5} {7} {8} {9}", result[1], result[2], result[3], result[4], result[6], result[7], date.ToShortDateString(), timzoneFixed, tz1, isDaylight);
                    CustomCalc(date.Year, date.Month, date.Day, p, latitude, longitude);
                }
            }


            MakeTable(latitude, longitude);

            var sunrise=new DateTime(y,m,d,6,59,0);//05:18
            
            var sunset = new DateTime(y, m, d, 19, 54, 0);//19:37
            var endOfTwlight=new DateTime(y,m,d+1,0,28,0);

            var sunriseOneDayAfter = new DateTime(y, m, d+1, 3,59, 0);//19:37
            //GetSunSetAndSunRise(new DateTime(y, m, d), latitude, longitude);

            var farjmin = CalculateFarjMinimum(latitude, new DateTime(y,m,d));
            var ishaAhmer = CalculateIshaMinimumAhmer(latitude, new DateTime(y, m, d));
            var ishaAbyad = CalculateIshaMinimumAbyad(latitude, new DateTime(y, m, d));
            var ishaGeneral = CalculateIshaMinimumGeneral(latitude, new DateTime(y, m, d));

            var farj = sunrise.AddMinutes(0- farjmin);
            var tr= 2 - longitude / 15;

            var isha1 = sunset.AddMinutes(ishaAhmer);
            var isha2 = sunset.AddMinutes(ishaAbyad);
            var isha3 = sunset.AddMinutes(ishaGeneral);

            var oneSeventh = (sunrise.Subtract(sunset).TotalMinutes)/7;
            //var farjrHighLat = sunset.AddMinutes(oneSeventh*6);
            var farjrHighLat = sunrise.AddMinutes(0-oneSeventh);
            var ishaHighLat = sunset.AddMinutes(oneSeventh);

            var oneseventhWithTwilight= (sunriseOneDayAfter.Subtract(endOfTwlight).TotalMinutes)/ 7;
            var farjrHighLatTwilight = sunset.AddMinutes(oneseventhWithTwilight * 6);
            var ishaHighLatTwilight = sunset.AddMinutes(oneseventhWithTwilight);

            DateTime cc = DateTime.Now;
            y = cc.Year;
            m = cc.Month;
            d = cc.Day;
            
        }

        private static void HijriCalendaretest()
        {
            DateTime myDT = new DateTime(2002, 4, 3, new GregorianCalendar());

            // Creates an instance of the HijriCalendar.
            HijriCalendar myCal = new HijriCalendar();

            // Displays the values of the DateTime.
            Console.WriteLine("April 3, 2002 of the Gregorian calendar equals the following in the Hijri calendar:");
            DisplayValues(myCal, myDT);

            // Adds two years and ten months.
            myDT = myCal.AddYears(myDT, 2);
            myDT = myCal.AddMonths(myDT, 10);

            // Displays the values of the DateTime.
            Console.WriteLine("After adding two years and ten months:");
            DisplayValues(myCal, myDT);
        }

        private static void DisplayValues(Calendar myCal, DateTime myDT)
        {
            string monthName = new DateTime(2010, 8, 1).ToString("MMM", CultureInfo.InvariantCulture);
            //UmAlQuraCalendar
            var dt = myCal.ToDateTime(1438, 1, 1,0,0,0,0);
            string fullMonthName = new DateTime(2015, 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("ar-SA"));
            Console.WriteLine("   Era:        {0}", myCal.GetEra(myDT));
            Console.WriteLine("   Year:       {0}", myCal.GetYear(myDT));
            Console.WriteLine("   Month:      {0}", myCal.GetMonth(myDT));
            Console.WriteLine("   DayOfYear:  {0}", myCal.GetDayOfYear(myDT));
            Console.WriteLine("   DayOfMonth: {0}", myCal.GetDayOfMonth(myDT));
            Console.WriteLine("   DayOfWeek:  {0}", myCal.GetDayOfWeek(myDT));
            Console.WriteLine();
        }
        public static bool UseDayLightaving(DateTime dateTime)
        {
            return TimeZone.CurrentTimeZone.IsDaylightSavingTime(dateTime);
        }
        static int timzoneFixed;
        static int tz1;
        private static bool isDaylight;
        private static string[] CustomCalc(int y, int m, int d, PrayTimeNS.PrayTime p, double latitude, double longitude)
        {
            //
            timzoneFixed = TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(y, m, d).AddDays(1)).Hours;
            tz1 = TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(y, m, d)).Hours;
            isDaylight = TimeZoneInfo.Local.IsDaylightSavingTime(new DateTime(y, m, d));

            String[] s;

            p.setHighLatsMethod(2);// za svedsku
           
            p.setDhuhrMinutes(5);
            p.setMaghribMinutes(6);
            p.setAsrMethod(0);
            p.setCalcMethod(10);
            s = p.getDatePrayerTimes(y, m, d, latitude, longitude, timzoneFixed, 0);
            //Console.WriteLine("{0} {1} {2}", y,m,d);
            //for (int i = 0; i < s.Length; ++i)
            //{
            //    Console.WriteLine(s[i]);
            //}
            return s;
        }
        private static void CustomCalc(int y, int m, int d, PrayTime2.PrayTime p, double latitude, double longitude)
        {
            int tz;
            tz = TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(y, m, d)).Hours;
            String[] s;

            //p.setHighLatsMethod(2);// za svedsku

            p.setAsrMethod(0);
            p.setCalcMethod(3);
            s = p.getDatePrayerTimes(y, m, d, latitude, longitude, tz, 0);
            for (int i = 0; i < s.Length; ++i)
            {
                Console.WriteLine(s[i]);
            }
        }

        private static void MakeTable(double latitude, double longitude)
        {
            using (StreamWriter writer = new StreamWriter(String.Format(@"C:\Users\kurarm\Documents\IdM\pray{0}.txt", latitude)))
            {
                Console.SetOut(writer);
                Console.WriteLine("Latitude={0}", latitude);
                Console.WriteLine("Longitude={0}", longitude);
                Console.WriteLine(
                            "          farj     mFARJ     farjrHighLat     mIsha     isha3     ishaHighLat   isha1      isha2     farjMIN     ishaMIN ");
                for (DateTime date = new DateTime(2015, 1, 1);
                    date <= new DateTime(2015, 12, 31);
                    date = date.AddDays(1))
                {
                    //var sunriseandsunset = GetSunSetAndSunRise(date, latitude, longitude);
                    //var sunrise = sunriseandsunset.rise;
                    //var sunset = sunriseandsunset.set;
                    //var sunrise = DateTime.Parse(sunriseandsunset.sunrise);
                    //var sunset = DateTime.Parse(sunriseandsunset.sunset);
                    var sunriseandsunset = GetDataFromDB(date);
                    var diffSUnRiseSunSet = sunriseandsunset.sunset.Subtract(sunriseandsunset.sunrise);
                    var dayTotalMinutes = (24.00*60.00);
                    var dayNighDiff = diffSUnRiseSunSet.TotalMinutes/dayTotalMinutes;
                    var nightDayDiff = dayTotalMinutes/diffSUnRiseSunSet.TotalMinutes;

                    var sunrise = new DateTime(date.Year, date.Month, date.Day, sunriseandsunset.sunrise.Hours, sunriseandsunset.sunrise.Minutes, sunriseandsunset.sunrise.Seconds);
                    sunrise = sunrise.ToUniversalTime();
                    var sunset = new DateTime(date.Year, date.Month, date.Day, sunriseandsunset.sunset.Hours, sunriseandsunset.sunset.Minutes, sunriseandsunset.sunset.Seconds);
                    sunset = sunset.ToUniversalTime();
                    var mFARJ = new DateTime(date.Year, date.Month, date.Day, sunriseandsunset.farj.Hours, sunriseandsunset.farj.Minutes, sunriseandsunset.farj.Seconds);
                    mFARJ = mFARJ.ToUniversalTime();
                    var mIsha = new DateTime(date.Year, date.Month, date.Day, sunriseandsunset.isha.Hours, sunriseandsunset.isha.Minutes, sunriseandsunset.isha.Seconds);
                    mIsha = mIsha.ToUniversalTime();
                    //astrodataTimeLocationSun sunriseOneDayAfterAll = null;
                    //    sunriseOneDayAfterAll = GetSunSetAndSunRise(date.AddDays(1),latitude, longitude);
                    //var    sunriseOneDayAfterAll = GetSunSetAndSunRise(date.AddDays(1),latitude, longitude);

                    //var sunriseOneDayAfter = DateTime.Parse(sunriseOneDayAfterAll.sunrise);
                    DateTime sunriseOneDayAfter = date;
                    //if (date != new DateTime(2015, 12, 31))
                    //{
                    var tr = GetDataFromDB(date.AddDays(1));
                    var trold = GetDataFromDB(date.AddDays(-1));
                    sunriseOneDayAfter = new DateTime(tr.date.Year, tr.date.Month, tr.date.Day, tr.sunrise.Hours, tr.sunrise.Minutes, tr.sunrise.Seconds);
                    sunriseOneDayAfter = sunriseOneDayAfter.ToUniversalTime();
                    DateTime sunsetOneDayBefore= new DateTime(trold.date.Year, trold.date.Month, trold.date.Day, trold.sunset.Hours, trold.sunset.Minutes, trold.sunset.Seconds);
                    sunsetOneDayBefore = sunsetOneDayBefore.ToUniversalTime();
                    //}

                    var farjmin = CalculateFarjMinimum(latitude, date);
                    var ishaAhmer = CalculateIshaMinimumAhmer(latitude, date);
                    var ishaAbyad = CalculateIshaMinimumAbyad(latitude, date);
                    var ishaGeneral = CalculateIshaMinimumGeneral(latitude, date);

                    //Sunrise - (Sunrise - sunset of previous day)/ 7
                    //var farjMoonHighLat=sunrise.AddMinutes(-(sunrise.Subtract(sunsetOneDayBefore).TotalMinutes)/7);
                    //var rer= sunset.AddMinutes(-(sunrise.Subtract(sunsetOneDayBefore).TotalMinutes));
                    // var farjMoonHighLat1 = sunrise.AddMinutes(-(sunrise.Subtract(sunsetOneDayBefore).TotalMinutes) / 7);


                    var farj = sunrise.AddMinutes(0 - farjmin);
                    var isha1 = sunset.AddMinutes(ishaAhmer);
                    var isha2 = sunset.AddMinutes(ishaAbyad);
                    var isha3 = sunset.AddMinutes(ishaGeneral);

                    var oneSeventhNextDay = (sunriseOneDayAfter.Subtract(sunset).TotalMinutes) / 7;

                    //var farjrHighLat = sunset.AddMinutes(oneSeventhNextDay * 6);
                    var farjrHighLat = sunrise.AddMinutes(-(sunrise.Subtract(sunsetOneDayBefore).TotalMinutes) / 7);
                    var ishaHighLat = sunset.AddMinutes(oneSeventhNextDay);


                    if (farjrHighLat > farj)
                        farj = farjrHighLat;
                    if (ishaHighLat < isha3)
                        isha3 = ishaHighLat;

//                        If(subhsadiq < last1 / 7)
// Farj = last 1 / 7th
//else
// Farj = subhsadiq

                    //DateTime farjcSeason = DateTime.MinValue;
                    //DateTime ishaSeason = DateTime.MinValue;
                    //if (dayNighDiff < 0.54)
                    //    farjcSeason = farj;
                    //else
                    //    farjcSeason = farjrHighLat;

                    //if (dayNighDiff > 0.68)
                    //    ishaSeason = ishaHighLat;
                    //else
                    //    ishaSeason = isha3;


                    //var oneSeventh = (sunrise.Subtract(sunset).TotalMinutes) / 7;
                    //var farjrHighLat = sunrise.AddMinutes(0 - oneSeventh);
                    //var ishaHighLat = sunset.AddMinutes(oneSeventh);



                    //var farj = sunrise.AddMinutes(0 - farjmin);
                    //var isha1 = sunset.AddMinutes(ishaAhmer);
                    //var isha2 = sunset.AddMinutes(ishaAbyad);
                    //var isha3 = sunset.AddMinutes(ishaGeneral);

                    //var oneSeventh = (sunriseOneDayAfter.Subtract(sunset).TotalMinutes)/7;
                    //var farjrHighLat = sunset.AddMinutes(oneSeventh*6);
                    //var ishaHighLat = sunset.AddMinutes(oneSeventh);

                    //Console.WriteLine(
                    //    "{0}   {1}     {2}     {3}     {4}    {5}     {6}     {7}     {8}         {9}   {10} {11} {12} {13}",
                    //    //"{0}   {1}    {3}      {5}     {6}     {7}     {8}         {9}   {10}  {11}",
                    //    date.ToShortDateString(), farj.ToLocalTime().ToShortTimeString(), mFARJ.ToLocalTime().ToShortTimeString(), farjrHighLat.ToLocalTime().ToShortTimeString(),
                    //    mIsha.ToLocalTime().ToShortTimeString(),
                    //    isha3.ToLocalTime().ToShortTimeString(),
                    //    ishaHighLat.ToLocalTime().ToShortTimeString(),
                    //    isha1.ToLocalTime().ToShortTimeString(), isha2.ToLocalTime().ToShortTimeString(), Math.Round(farjmin, 1),
                    //    Math.Round(ishaGeneral, 1), diffSUnRiseSunSet.TotalMinutes,dayNighDiff,nightDayDiff);
                    Console.WriteLine(
                        "{0}  {1} {2} {3}     {4} {5} {6} {7} {8}     {9} {10}",
                        date.ToShortDateString(), farj.Subtract(mFARJ).Minutes, mFARJ.ToShortTimeString(),
                        farjrHighLat.Subtract(mFARJ).Minutes,
                        mIsha.ToShortTimeString(),
                        isha3.Subtract(mIsha).Minutes,
                        ishaHighLat.Subtract(mIsha).Minutes,
                        isha1.Subtract(mIsha).Minutes, isha2.Subtract(mIsha).Minutes,
                        farj.ToShortTimeString(),
                        farjrHighLat.ToShortTimeString());
                }
            }
        }

        private static double CalculateFarjMinimum(double lt, DateTime dateTime)
        {
            var A = 75 + (28.65/55)*Math.Abs(lt);
            var B = 75 + (19.44 / 55) * Math.Abs(lt);
            var C = 75 + (32.74 / 55) * Math.Abs(lt);
            var D = 75 + (48.1 / 55) * Math.Abs(lt);

            //var dateBase = new DateTime(dateTime.Year - 1, 12, 21);
            var dateBase = new DateTime(dateTime.Year - 1, 12, 21).ToUniversalTime();

            if (dateTime.Month.Equals(12) && dateTime.Day.Equals(21))
                dateBase= new DateTime(dateTime.Year, 12, 21).ToUniversalTime();
                //dateBase = new DateTime(dateTime.Year, 12, 21);

            var skillnadDate = dateTime.Subtract(dateBase);
            var DYY = skillnadDate.TotalDays;
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A)/91)*DYY;
            else if (DYY < 137)
                minimum = B + ((C - B)/46)*(DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C)/46) * (DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D)/46) * (DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C)/46) * (DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B)/91)*(DYY - 275);

            return minimum;
        }

        private static double CalculateIshaMinimumAhmer(double lt, DateTime dateTime)
        {
            var A = 62 + (17.4 / 55) * Math.Abs(lt);
            var B = 62 - (7.16 / 55) * Math.Abs(lt);
            var C = 62 + (5.12 / 55) * Math.Abs(lt);
            var D = 62 + (19.44 / 55) * Math.Abs(lt);

            var dateBase = new DateTime(dateTime.Year - 1, 12, 21).ToUniversalTime();
            //var dateBase = new DateTime(dateTime.Year - 1, 12, 21);

            if (dateTime.Month.Equals(12) && dateTime.Day.Equals(21))
                dateBase = new DateTime(dateTime.Year, 12, 21).ToUniversalTime();
                //dateBase = new DateTime(dateTime.Year, 12, 21);

            var skillnadDate = dateTime.Subtract(dateBase);
            var DYY = skillnadDate.TotalDays;
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A) / 91) * DYY;
            else if (DYY < 137)
                minimum = B + ((C - B) / 46) * (DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C) / 46) * (DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D) / 46) * (DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C) / 46) * (DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B) / 91) * (DYY - 275);

            return minimum;
        }
        private static double CalculateIshaMinimumAbyad(double lt, DateTime dateTime)
        {
            var A = 75 + (25.6 / 55) * Math.Abs(lt);
            var B = 75 + (7.16 / 55) * Math.Abs(lt);
            var C = 75 + (36.84 / 55) * Math.Abs(lt);
            var D = 75 + (81.84 / 55) * Math.Abs(lt);

            //var dateBase = new DateTime(dateTime.Year - 1, 12, 21);
            var dateBase = new DateTime(dateTime.Year - 1, 12, 21).ToUniversalTime();

            if (dateTime.Month.Equals(12) && dateTime.Day.Equals(21))
                dateBase = new DateTime(dateTime.Year, 12, 21).ToUniversalTime();
                //dateBase = new DateTime(dateTime.Year, 12, 21);

            var skillnadDate = dateTime.Subtract(dateBase);
            var DYY = skillnadDate.TotalDays;
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A) / 91) * DYY;
            else if (DYY < 137)
                minimum = B + ((C - B) / 46) * (DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C) / 46) * (DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D) / 46) * (DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C) / 46) * (DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B) / 91) * (DYY - 275);

            return minimum;
        }
        private static double CalculateIshaMinimumGeneral(double lt, DateTime dateTime)
        {
            double A = 75 + (25.6 / 55.0) * Math.Abs(lt);
            double B = 75 + (2.05 / 55.0) * Math.Abs(lt);
            double C = 75 - (9.21 / 55.0) * Math.Abs(lt);
            double D = 75 + (6.14 / 55.0) * Math.Abs(lt);

            //var dateBase = new DateTime(dateTime.Year, 12, 21);
            var dateBase = new DateTime(dateTime.Year - 1, 12, 21).ToUniversalTime();

            if (dateTime.Month.Equals(12) && dateTime.Day.Equals(21))
                dateBase = new DateTime(dateTime.Year, 12, 21).ToUniversalTime();
                //dateBase = new DateTime(dateTime.Year, 12, 21);

            var skillnadDate = dateTime.Subtract(dateBase);
            var DYY = skillnadDate.TotalDays;
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A) / 91) * DYY;
            else if (DYY < 137)
                minimum = B + ((C - B) / 46) * (DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C) / 46) * (DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D) / 46) * (DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C) / 46) * (DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B) / 91) * (DYY - 275);

            return minimum;
        }

        private static suntable GetDataFromDB(DateTime date)
        {
            using (var db = new Model1())
            {
                return db.suntable.FirstOrDefault(u => u.date.Equals(date));
            }
        }
        private static Date GetSunSetAndSunRise(DateTime date, double latitute, double longitute)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var str = String.Format("http://api.met.no/weatherapi/sunrise/1.0/?lat={0};lon={1};date={2}", latitute.ToString(new CultureInfo("en-US")),
                //    longitute.ToString(new CultureInfo("en-US")), date.ToShortDateString());
                //var t =
                //    client.GetAsync(str);
                ////string incomingText = t.Result.Content.ReadAsStringAsync().Result;
                //XmlSerializer serializer = new XmlSerializer(typeof(astrodata));
                //astrodataTimeLocationSun result = null;
                //using (Stream stream = t.Result.Content.ReadAsStreamAsync().Result)
                //{
                //     result =((astrodata)serializer.Deserialize(stream)).time.location.sun;
                //    var sert=TimeZone.CurrentTimeZone.GetUtcOffset(date).Hours;
                //    result.rise= result.rise.AddHours(sert);
                //    result.set=result.set.AddHours(sert);
                //}

                client.BaseAddress = new Uri("http://api.sunrise-sunset.org/");

                // New code:
                var response1 = client.GetAsync($"json?lat={latitute}&lng={longitute}&date={date}&formatted=0");
                if (response1.Result.IsSuccessStatusCode)
                {
                    var product = response1.Result.Content.ReadAsAsync<RootObject>();
                }


                //client.BaseAddress = new Uri("http://api.geonames.org/");

                ////New code:
                //MoonPak result = null;
                //var strings = String.Format("timezoneJSON?lat={0}&lng={1}&username=demo&date={2}", latitute.ToString(new CultureInfo("en-US")),
                //    longitute.ToString(new CultureInfo("en-US")), date.ToShortDateString());

                //var response2 = client.GetAsync(strings);
                //if (response2.Result.IsSuccessStatusCode)
                //{
                //    result = response2.Result.Content.ReadAsAsync<MoonPak>().Result;
                //}
                //return result.dates[0];


                return null;

            }
            
        }

    }
    
    public class Results
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string solar_noon { get; set; }
        public int day_length { get; set; }
        public string civil_twilight_begin { get; set; }
        public string civil_twilight_end { get; set; }
        public string nautical_twilight_begin { get; set; }
        public string nautical_twilight_end { get; set; }
        public string astronomical_twilight_begin { get; set; }
        public string astronomical_twilight_end { get; set; }
    }

    public class RootObject
    {
        public Results results { get; set; }
        public string status { get; set; }
    }
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class astrodata
    {

        private astrodataMeta metaField;

        private astrodataTime timeField;

        /// <remarks/>
        public astrodataMeta meta
        {
            get
            {
                return this.metaField;
            }
            set
            {
                this.metaField = value;
            }
        }

        /// <remarks/>
        public astrodataTime time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class astrodataMeta
    {

        private string licenseurlField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string licenseurl
        {
            get
            {
                return this.licenseurlField;
            }
            set
            {
                this.licenseurlField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class astrodataTime
    {

        private astrodataTimeLocation locationField;

        private System.DateTime dateField;

        /// <remarks/>
        public astrodataTimeLocation location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class astrodataTimeLocation
    {

        private astrodataTimeLocationSun sunField;

        private astrodataTimeLocationMoon moonField;

        private decimal latitudeField;

        private decimal longitudeField;

        /// <remarks/>
        public astrodataTimeLocationSun sun
        {
            get
            {
                return this.sunField;
            }
            set
            {
                this.sunField = value;
            }
        }

        /// <remarks/>
        public astrodataTimeLocationMoon moon
        {
            get
            {
                return this.moonField;
            }
            set
            {
                this.moonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class astrodataTimeLocationSun
    {

        private astrodataTimeLocationSunNoon noonField;

        private System.DateTime riseField;

        private System.DateTime setField;

        /// <remarks/>
        public astrodataTimeLocationSunNoon noon
        {
            get
            {
                return this.noonField;
            }
            set
            {
                this.noonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime rise
        {
            get
            {
                return this.riseField;
            }
            set
            {
                this.riseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime set
        {
            get
            {
                return this.setField;
            }
            set
            {
                this.setField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class astrodataTimeLocationSunNoon
    {

        private decimal altitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal altitude
        {
            get
            {
                return this.altitudeField;
            }
            set
            {
                this.altitudeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class astrodataTimeLocationMoon
    {

        private string phaseField;

        private System.DateTime riseField;

        private System.DateTime setField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string phase
        {
            get
            {
                return this.phaseField;
            }
            set
            {
                this.phaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime rise
        {
            get
            {
                return this.riseField;
            }
            set
            {
                this.riseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime set
        {
            get
            {
                return this.setField;
            }
            set
            {
                this.setField = value;
            }
        }
    }





    public class calculate
    {
        public string sunrise { get; set; }
        public double lng { get; set; }
        public string countryCode { get; set; }
        public int gmtOffset { get; set; }
        public int rawOffset { get; set; }
        public string sunset { get; set; }
        public string timezoneId { get; set; }
        public int dstOffset { get; set; }
        public string countryName { get; set; }
        public string time { get; set; }
        public double lat { get; set; }
    }

    public class Date
    {
        public string sunset { get; set; }
        public string sunrise { get; set; }
        public string date { get; set; }
    }

    public class MoonPak
    {
        public string time { get; set; }
        public string countryName { get; set; }
        public List<Date> dates { get; set; }
        public string sunset { get; set; }
        public int rawOffset { get; set; }
        public int dstOffset { get; set; }
        public string countryCode { get; set; }
        public int gmtOffset { get; set; }
        public double lng { get; set; }
        public string sunrise { get; set; }
        public string timezoneId { get; set; }
        public double lat { get; set; }
    }
}




