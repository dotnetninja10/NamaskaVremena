using System;
namespace PrayerTimes.Types
{
    public class DateComponent
    {
        public int TimeZoneUtcOffset { get; private set; }
        public DateTime CalculationDate { get; private set; }
        public TimeFormats TimeFormat { get; private set; }

        public DateComponent(DateTime calculationDate, int timeZoneUtcOffset)
        {
            this.CalculationDate = calculationDate;
            this.TimeZoneUtcOffset = timeZoneUtcOffset;
            this.TimeFormat = TimeFormats.Hour24;
        }
        public DateComponent(DateTime calculationDate, string timeZone)
        {
            this.CalculationDate = calculationDate;
            this.TimeZoneUtcOffset = TimeZoneInfo.FindSystemTimeZoneById(timeZone).GetUtcOffset(calculationDate.AddDays(1)).Hours;
            this.TimeFormat = TimeFormats.Hour24;
        }
        public DateComponent(DateTime calculationDate, int timeZoneUtcOffset, TimeFormats timeFormat) : this(calculationDate, timeZoneUtcOffset)
        {
            this.TimeFormat = timeFormat;
        }
        public DateComponent(DateTime calculationDate, string timeZone, TimeFormats timeFormat) : this(calculationDate, timeZone)
        {
            this.TimeFormat = timeFormat;
        }
    }
}
