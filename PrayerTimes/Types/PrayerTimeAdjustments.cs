namespace PrayerTimes.Types
{
    public class PrayerTimeAdjustments
    {
        public readonly short Imsak = 0; //default 10 minutes of imsak before Fajr
        public readonly short Fajr = 0;
        public readonly short Dhuhr = 0;
        public readonly short Asr = 0;
        public readonly short Maghrib = 0;
        public readonly short Isha = 0;

        public PrayerTimeAdjustments()
        {
            this.Imsak = -10;
            this.Fajr = 0;
            this.Dhuhr = 0;
            this.Asr = 0;
            this.Maghrib = 0;
            this.Isha = 0;
        }
        public PrayerTimeAdjustments(short imsak, short fajr, short dhuhr, short asr, short maghrib, short isha)
        {
            this.Imsak = imsak;
            this.Fajr = fajr;
            this.Dhuhr = dhuhr;
            this.Asr = asr;
            this.Maghrib = maghrib;
            this.Isha = isha;
        }
    }
}
