﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayerTimes.Types
{
    public class DateComponent
    {
        public int TimeZoneUtcOffset { get; private set; }
        public DateTime CalculationDate { get; private set; }
        public TimeFormats TimeFormat { get; private set; }
        //JUlianDate
        //public double JulianDate { get; private set; }

        public DateComponent(DateTime calculationDate, int timeZoneUtcOffset)
        {
            this.CalculationDate = calculationDate;
            this.TimeZoneUtcOffset = timeZoneUtcOffset;
            this.TimeFormat = TimeFormats.Hour24;
            //this.JulianDate = ToJulianDate(calculationDate.Year, calculationDate.Month, calculationDate.Day);
        }
        public DateComponent(DateTime calculationDate, string timeZone)
        {
            this.CalculationDate = calculationDate;
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            this.TimeZoneUtcOffset = timeZoneId.GetUtcOffset(calculationDate.AddDays(1)).Hours; ;
            this.TimeFormat = TimeFormats.Hour24;
            //this.JulianDate = ToJulianDate(calculationDate.Year, calculationDate.Month, calculationDate.Day);
        }
        public DateComponent(DateTime calculationDate, int timeZoneUtcOffset, TimeFormats timeFormat) : this(calculationDate, timeZoneUtcOffset)
        {
            this.TimeFormat = timeFormat;
        }

        //private double ToJulianDate(int year, int month, int day)
        //{
        //    if (month <= 2)
        //    {
        //        year -= 1;
        //        month += 12;
        //    }
        //    var A = (double)Math.Floor(year / 100.0);
        //    var B = 2 - A + Math.Floor(A / 4.00);

        //    var JD = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.500;
        //    return JD;

        //}

    }
}
