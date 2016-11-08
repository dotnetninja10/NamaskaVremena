using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrayerTimes;
using PrayerTimes.Types;

namespace PrayerTimes.Tests
{
    [TestClass]
    public class PreyerTimeCalculatorTest
    {

        //[ClassInitialize]
        //public void Setup()
        //{
        //}

        [TestMethod]
        public void CalculateTimesWithNORTH_AMERICAFor2016_10_15ForLat43_Long43()
        {
            var zones = TimeZoneInfo.GetSystemTimeZones();
            var calculationDate = new DateTime(2017, 13, 1);
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, 43, 0),
                   CalculationMethods.NORTH_AMERICA);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 10, 15), 1));
            Assert.AreEqual(times.Imsak, "02:54");
            Assert.AreEqual(times.Fajr, "03:04");
            Assert.AreEqual(times.Sunrise, "04:22");
            Assert.AreEqual(times.Dhuhr, "09:54");
            Assert.AreEqual(times.Asr, "12:55");
            Assert.AreEqual(times.Sunset, "15:25");
            Assert.AreEqual(times.Maghrib, "15:25");
            Assert.AreEqual(times.Isha, "16:43");
            Assert.AreEqual(times.Midnight, "23:45");
        }
        [TestMethod]
        public void CalculateTimesWithNORTH_AMERICAFor2016_10_15ForLat43_Long43AndTimeAdjustments()
        {
            var coordinates = new Coordinates(43, 43, 0);
            var calculationparameters = new CalculationParameters(CalculationMethods.NORTH_AMERICA,
                AsrMethodMadhab.Shafii, 
                HighLatitudeRule.None, 
                MidnightMethod.Standard, 
                new PrayerTimeAdjustments(-5, 5,10, -5, 5, 10));

            var calculator = new PrayerTimesCalculator(coordinates,calculationparameters);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 10, 15), 1));

            Assert.AreEqual(times.Imsak, "03:04");
            Assert.AreEqual(times.Fajr, "03:09");
            Assert.AreEqual(times.Sunrise, "04:22");
            Assert.AreEqual(times.Dhuhr, "10:04");
            Assert.AreEqual(times.Asr, "12:50");
            Assert.AreEqual(times.Sunset, "15:25");
            Assert.AreEqual(times.Maghrib, "15:30");
            Assert.AreEqual(times.Isha, "16:53");
            Assert.AreEqual(times.Midnight, "23:45");
        }
        [TestMethod]
        public void CalculateTimesWithMekkahFor2016_12_31ForLat43_Long43()
        {

            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, 43, 0),
                   CalculationMethods.UMM_AL_QURA);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 12, 31), 0));
            Assert.AreEqual(times.Imsak, "02:44");
            Assert.AreEqual(times.Fajr, "02:54");
            Assert.AreEqual(times.Sunrise, "04:39");
            Assert.AreEqual(times.Dhuhr, "09:11");
            Assert.AreEqual(times.Asr, "11:25");
            Assert.AreEqual(times.Sunset, "13:43");
            Assert.AreEqual(times.Maghrib, "13:43");
            Assert.AreEqual(times.Isha, "15:13");
            Assert.AreEqual(times.Midnight, "00:03");
        }
        [TestMethod]
        public void CalculateTimesWithMekkahFor2016_12_31ForLat43_Long43WitTimeAdjustments()
        {
            var coordinates = new Coordinates(43, 43, 0);
            var calculationparameters = new CalculationParameters(CalculationMethods.UMM_AL_QURA,
                AsrMethodMadhab.Shafii,
                HighLatitudeRule.None,
                MidnightMethod.Standard,
                new PrayerTimeAdjustments(-5, 5,10, -5, 5, 10));

            var calculator = new PrayerTimesCalculator(coordinates,calculationparameters);

            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 12, 31), 0));
            Assert.AreEqual(times.Imsak, "02:54");
            Assert.AreEqual(times.Fajr, "02:59");
            Assert.AreEqual(times.Sunrise, "04:39");
            Assert.AreEqual(times.Dhuhr, "09:21");
            Assert.AreEqual(times.Asr, "11:20");
            Assert.AreEqual(times.Sunset, "13:43");
            Assert.AreEqual(times.Maghrib, "13:48");
            Assert.AreEqual(times.Isha, "15:23");
            Assert.AreEqual(times.Midnight, "00:03");
        }
        [TestMethod]
        public void CalculateTimesWithMekkahFor2016_03_31ForLat43_Long43()
        {

            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, 43, 0),
                   CalculationMethods.UMM_AL_QURA);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 3, 31), 1));
            Assert.AreEqual(times.Imsak, "02:01");
            Assert.AreEqual(times.Fajr, "02:11");
            Assert.AreEqual(times.Sunrise, "03:52");
            Assert.AreEqual(times.Dhuhr, "10:12");
            Assert.AreEqual(times.Asr, "13:46");
            Assert.AreEqual(times.Sunset, "16:33");
            Assert.AreEqual(times.Maghrib, "16:33");
            Assert.AreEqual(times.Isha, "18:03");
            Assert.AreEqual(times.Midnight, "00:04");
        }
        [TestMethod]
        public void CalculateTimesWithJafariFor2016_03_31ForLat43_Long43()
        {

            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, 43, 0),
                   CalculationMethods.JAFARI);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 3, 31), 1));
            Assert.AreEqual(times.Imsak, "02:16");
            Assert.AreEqual(times.Fajr, "02:26");
            Assert.AreEqual(times.Sunrise, "03:52");
            Assert.AreEqual(times.Dhuhr, "10:12");
            Assert.AreEqual(times.Asr, "13:46");
            Assert.AreEqual(times.Sunset, "16:33");
            Assert.AreEqual(times.Maghrib, "16:51");
            Assert.AreEqual(times.Isha, "17:48");
            Assert.AreEqual(times.Midnight, "23:22");
        }
        [TestMethod]
        public void CalculateTimesWithJafariFor2016_06_20ForLat43_Long43()
        {

            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, 43, 0),
                   CalculationMethods.JAFARI);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 6, 20), 4));
            Assert.AreEqual(times.Imsak, "03:21");
            Assert.AreEqual(times.Fajr, "03:31");
            Assert.AreEqual(times.Sunrise, "05:29");
            Assert.AreEqual(times.Dhuhr, "13:10");
            Assert.AreEqual(times.Asr, "17:15");
            Assert.AreEqual(times.Sunset, "20:51");
            Assert.AreEqual(times.Maghrib, "21:12");
            Assert.AreEqual(times.Isha, "22:29");
            Assert.AreEqual(times.Midnight, "23:03");
        }
        [TestMethod]
        public void CalculateTimesWithNORTH_AMERICAFor2016_12_31ForLat43_Long43()
        {

            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, 43, 0),
                   CalculationMethods.NORTH_AMERICA);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 12, 31), 0));
            Assert.AreEqual(times.Imsak, "03:04");
            Assert.AreEqual(times.Fajr, "03:14");
            Assert.AreEqual(times.Sunrise, "04:39");
            Assert.AreEqual(times.Dhuhr, "09:11");
            Assert.AreEqual(times.Asr, "11:25");
            Assert.AreEqual(times.Sunset, "13:43");
            Assert.AreEqual(times.Maghrib, "13:43");
            Assert.AreEqual(times.Isha, "15:09");
            Assert.AreEqual(times.Midnight, "00:03");
        }
        [TestMethod]
        public void CalculateTimesWithMuslimWorldLeagueFor2016_11_1ForLat43_LongMinus80()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //offset=-4
            var offset = timeZone.GetUtcOffset(new DateTime(2016, 11, 1).AddDays(1)).Hours;
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, -80, 0),
                   CalculationMethods.MUSLIM_WORLD_LEAGUE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 11, 1), offset));
            Assert.AreEqual(times.Imsak, "06:09");
            Assert.AreEqual(times.Fajr, "06:19");
            Assert.AreEqual(times.Sunrise, "07:55");
            Assert.AreEqual(times.Dhuhr, "13:04");
            Assert.AreEqual(times.Asr, "15:47");
            Assert.AreEqual(times.Sunset, "18:11");
            Assert.AreEqual(times.Maghrib, "18:11");
            Assert.AreEqual(times.Isha, "19:42");
            Assert.AreEqual(times.Midnight, "23:43");
        }
        [TestMethod]
        public void CalculateTimesWithMuslimWorldLeagueFor2016_11_17ForLat43_LongMinus80()
        {
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, -80, 0),
                   CalculationMethods.MUSLIM_WORLD_LEAGUE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 11, 17), -4));
            Assert.AreEqual(times.Imsak, "06:27");
            Assert.AreEqual(times.Fajr, "06:37");
            Assert.AreEqual(times.Sunrise, "08:16");
            Assert.AreEqual(times.Dhuhr, "13:05");
            Assert.AreEqual(times.Asr, "15:34");
            Assert.AreEqual(times.Sunset, "17:54");
            Assert.AreEqual(times.Maghrib, "17:54");
            Assert.AreEqual(times.Isha, "19:28");
            Assert.AreEqual(times.Midnight, "23:45");
        }
        [TestMethod]
        public void CalculateTimesWithMuslimWorldLeague2016_03_27ForLat43_LongMinus80()
        {
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, -80, 0),
                   CalculationMethods.MUSLIM_WORLD_LEAGUE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 3, 27), -3));
            Assert.AreEqual(times.Imsak, "06:23");
            Assert.AreEqual(times.Fajr, "06:33");
            Assert.AreEqual(times.Sunrise, "08:10");
            Assert.AreEqual(times.Dhuhr, "14:25");
            Assert.AreEqual(times.Asr, "17:56");
            Assert.AreEqual(times.Sunset, "20:41");
            Assert.AreEqual(times.Maghrib, "20:41");
            Assert.AreEqual(times.Isha, "22:12");
            Assert.AreEqual(times.Midnight, "00:06");
        }
        [TestMethod]
        public void CalculateTimesWithMuslimWorldLeagueFor2016_04_27ForLat43_LongMinus80()
        {
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(43, -80, 0),
                   CalculationMethods.MUSLIM_WORLD_LEAGUE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 4, 27), -3));
            Assert.AreEqual(times.Imsak, "05:20");
            Assert.AreEqual(times.Fajr, "05:30");
            Assert.AreEqual(times.Sunrise, "07:19");
            Assert.AreEqual(times.Dhuhr, "14:18");
            Assert.AreEqual(times.Asr, "18:10");
            Assert.AreEqual(times.Sunset, "21:17");
            Assert.AreEqual(times.Maghrib, "21:17");
            Assert.AreEqual(times.Isha, "22:59");
            Assert.AreEqual(times.Midnight, "23:58");
        }
        [TestMethod]
        public void CalculateTimesWithMoonSightFor2016_11_1ForVasteras()
        {
            var sarajevoTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var offset = sarajevoTime.GetUtcOffset(new DateTime(2016, 11, 1).AddDays(1)).Hours;
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(59.598531,16.512003,0),
                   CalculationMethods.MOON_SIGHTING_COMMITTEE);
            var times=calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 11, 1), offset));
            Assert.AreEqual(times.Imsak,"05:25");
            Assert.AreEqual(times.Fajr, "05:35");
            Assert.AreEqual(times.Sunrise, "07:15");
            Assert.AreEqual(times.Dhuhr, "11:43");
            Assert.AreEqual(times.Asr, "13:29");
            Assert.AreEqual(times.Sunset, "15:59");
            Assert.AreEqual(times.Maghrib, "16:02");
            Assert.AreEqual(times.Isha, "17:27");
            Assert.AreEqual(times.Midnight, "23:43");
        }
        [TestMethod]
        public void CalculateTimesWithMoonSightFor2016_1_10ForVasteras()
        {
            var sarajevoTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var offset = sarajevoTime.GetUtcOffset(new DateTime(2016, 1, 10).AddDays(1)).Hours;
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(59.598531, 16.512003, 0),
                   CalculationMethods.MOON_SIGHTING_COMMITTEE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 1, 10), offset));
            Assert.AreEqual(times.Imsak, "06:52");
            Assert.AreEqual(times.Fajr, "07:02");
            Assert.AreEqual(times.Sunrise, "08:46");
            Assert.AreEqual(times.Dhuhr, "12:06");
            Assert.AreEqual(times.Asr, "13:06");
            Assert.AreEqual(times.Sunset, "15:17");
            Assert.AreEqual(times.Maghrib, "15:20");
            Assert.AreEqual(times.Isha, "16:54");
            Assert.AreEqual(times.Midnight, "00:08");
        }
   
     
        [TestMethod]
        public void CalculateTimesWithMoonSightFor2016_10_29ForVasteras()
        {
            var sarajevoTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var offset = sarajevoTime.GetUtcOffset(new DateTime(2016, 10, 29).AddDays(1)).Hours;
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(59.598531, 16.512003, 0),
                   CalculationMethods.MOON_SIGHTING_COMMITTEE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 10, 29), offset));
            Assert.AreEqual(times.Imsak, "06:17");
            Assert.AreEqual(times.Fajr, "06:27");
            Assert.AreEqual(times.Sunrise, "08:08");
            Assert.AreEqual(times.Dhuhr, "12:43");
            Assert.AreEqual(times.Asr, "14:35");
            Assert.AreEqual(times.Sunset, "17:06");
            Assert.AreEqual(times.Maghrib, "17:09");
            Assert.AreEqual(times.Isha, "18:34");
            Assert.AreEqual(times.Midnight, "23:43");
        }
        [TestMethod]
        public void CalculateTimesWithMoonSightFor2016_10_30ForVasteras()
        {
            var sarajevoTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var offset = sarajevoTime.GetUtcOffset(new DateTime(2016, 10, 30).AddDays(1)).Hours;
            var calculator =
               new PrayerTimesCalculator(
                   new Coordinates(59.598531, 16.512003, 0),
                   CalculationMethods.MOON_SIGHTING_COMMITTEE);
            var times = calculator.GetPrayerTimesForDate(new DateComponent(new DateTime(2016, 10, 30), offset));
            Assert.AreEqual(times.Imsak, "05:20");
            Assert.AreEqual(times.Fajr, "05:30");
            Assert.AreEqual(times.Sunrise, "07:10");
            Assert.AreEqual(times.Dhuhr, "11:43");
            Assert.AreEqual(times.Asr, "13:33");
            Assert.AreEqual(times.Sunset, "16:04");
            Assert.AreEqual(times.Maghrib, "16:07");
            Assert.AreEqual(times.Isha, "17:32");
            Assert.AreEqual(times.Midnight, "23:43");
        }
    }
}
