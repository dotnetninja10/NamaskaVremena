using PrayerTimes.Types;

namespace PrayerTimes.Utilities
{
    public class PrayerHours
    {
        public PrayerHours(double imsak,double fajr, double sunrise, double dhuhr, double asr, double sunset, double maghrib, double isha, double midnight,TimeFormats timeFormat)
        {
            this.Imsak = imsak.DoubleToString(timeFormat);
            this.Fajr=fajr.DoubleToString(timeFormat);
            this.Sunrise=sunrise.DoubleToString(timeFormat);
            this.Dhuhr=dhuhr.DoubleToString(timeFormat);
            this.Asr=asr.DoubleToString(timeFormat);
            this.Sunset=sunset.DoubleToString(timeFormat);
            this.Maghrib=maghrib.DoubleToString(timeFormat);
            this.Isha=isha.DoubleToString(timeFormat);
            this.Midnight=midnight.DoubleToString(timeFormat);

        }

        public string Imsak { get; private set; }
        public string Fajr { get; private set; }
        public string Sunrise { get; private set; }
        public string Dhuhr { get; private set; }
        public string Asr { get; private set; }
        public string Sunset { get; private set; }
        public string Maghrib { get; private set; }
        public string Isha { get; private set; }
        public string Midnight { get; private set; }
    }
}