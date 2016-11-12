using System;
using System.Globalization;
using PrayerTimes.Types;


namespace PrayerTimes.Utilities
{
    public static class TimeUtilities
    {
        private const string InvalidTime = "-----";

        public static double TimeDiff(double time1, double time2)
        {
            return FixHour(time2 - time1);
        }
        public static double Julian(int year, int month, int day)
        {
            if (month <= 2)
            {
                year -= 1;
                month += 12;
            }
            double A = Math.Floor(year / 100.0);
            double B = 2 - A + Math.Floor(A / 4);

            double JD = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
            return JD;
        }
        public static string DoubleToString(this double time, TimeFormats timeFormat)
        {
            switch (timeFormat)
            {
                case TimeFormats.Hour24:
                    return time.FloatToTime24();
                case TimeFormats.Hour12:
                    return time.FloatToTime12(true);
                case TimeFormats.Hour12hNS:
                    return time.FloatToTime12NS();
                case TimeFormats.Hfloat:
                    return time.ToString(CultureInfo.InvariantCulture);
                default:
                    return time.FloatToTime24();
            }
        }

        private static string FloatToTime12NS(this double time)
        {
            return FloatToTime12(time, true);
        }

        private static string FloatToTime24(this double time)
        {
            if (time < 0 || double.IsNaN(time))
                return InvalidTime;
            time = FixHour(time + 0.5 / 60); // add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            return TwoDigitsFormat((int)hours) + ":" + TwoDigitsFormat((int)minutes);
        }

        private static string FloatToTime12(this double time, bool noSuffix)
        {
            if (time < 0 || double.IsNaN(time))
                return InvalidTime;
            time = FixHour(time + 0.5 / 60); // add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            string suffix = hours >= 12 ? " pm" : " am";
            hours = (hours + 12 - 1) % 12 + 1;
            return ((int)hours) + ":" + TwoDigitsFormat((int)minutes) + (noSuffix ? "" : suffix);
        }

        private static string TwoDigitsFormat(int num)
        {
            return (num < 10) ? "0" + num : num + "";
        }

        public static double FixHour(double a)
        {
            return Fix(a, 24);
        }

        private static double Fix(double a, int b)
        {
            a = a - b * (Math.Floor(a / b));
            return (a < 0) ? a + b : a;
        }
    }
}
