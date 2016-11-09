using System;
using PrayerTimes.Types;
namespace PrayerTimes.Utilities
{
    public class AstronomicalCalculations
    {
        private static MoonsightCalculationMethod _moonsightCalculationMethod;
        public AstronomicalCalculations(CalculationMethods calculationMethod)
        {
            if (calculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE)
                _moonsightCalculationMethod = new MoonsightCalculationMethod();
        }

        private struct SunAngle
        {
            public readonly double Declination;
            public readonly double Equation;

            public SunAngle(double d, double e)
            {
                Declination = d;
                Equation = e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asrFactor">Add 1 for shadow lenght</param>
        /// <param name="time"></param>
        /// <param name="julianDate"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public double AsrTime(int asrFactor, double time, double julianDate, double latitude)
        {
            var decl = SunPosition(julianDate + time).Declination;
            var angle = -MathUtilities.ArcCot(asrFactor + MathUtilities.Tan(Math.Abs(latitude - decl)));
            return SunAngleTime(angle, time, julianDate, latitude);
        }
        //public static double SunAngleTime(double angle, double time, double julianDate, double latitude, string direction = "NODIR")
        public double SunAngleTime(double angle, double time, double julianDate, double latitude, bool isCcwDirection = false)
        {
            var decl = SunPosition(julianDate + time).Declination;
            var noon = MidDay(time, julianDate);
            var t = 1 / 15.0 *
                    MathUtilities.ArcCos((-MathUtilities.Sin(angle) - MathUtilities.Sin(decl) * MathUtilities.Sin(latitude)) / (MathUtilities.Cos(decl) * MathUtilities.Cos(latitude)));
            return noon + (isCcwDirection ? -t : t);
        }
        private static SunAngle SunPosition(double jd)
        {
            double D = jd - 2451545.0;
            double g = FixAngle(357.529 + 0.98560028 * D);
            double q = FixAngle(280.459 + 0.98564736 * D);
            double L = FixAngle(q + 1.915 * MathUtilities.Sin(g) + 0.020 * MathUtilities.Sin(2 * g));

            double R = 1.00014 - 0.01671 * MathUtilities.Cos(g) - 0.00014 * MathUtilities.Cos(2 * g);
            double e = 23.439 - 0.00000036 * D;
            var RA = MathUtilities.ArcTan2(MathUtilities.Cos(e) * MathUtilities.Sin(L), MathUtilities.Cos(L)) / 15;
            var eqt = q / 15 - TimeUtilities.FixHour(RA);
            var decl = MathUtilities.ArcSin(MathUtilities.Sin(e) * MathUtilities.Sin(L));

            return new SunAngle(decl, eqt);

        }
        public double MoonSightIsha(double sunset, double latitude, DateTime calculationDate)
        {
            var minutes = _moonsightCalculationMethod.CalculateIshaMinimumGeneral(latitude, calculationDate);
            return sunset + (minutes / 60);
        }

        public double MoonSightFarj(double sunrise, double latitude, DateTime calculationDate)
        {
            var minutes = _moonsightCalculationMethod.CalculateFarjMinimumGeneral(latitude, calculationDate);
            return sunrise - (minutes / 60);
        }
        public double MidDay(double time, double julianDate)
        {
            var eqt = SunPosition(julianDate + time).Equation;
            var noon = TimeUtilities.FixHour(12 - eqt);
            return noon;
        }

        private static double NightPortion(double angle, double night, HighLatitudeRule highLatitudeRule)
        {
            var portion = 0.0;

            if (highLatitudeRule == HighLatitudeRule.AngleBased)
                portion = 1.0 / 60.0 * angle;
            if (highLatitudeRule == HighLatitudeRule.OneSeventh)
                portion = 1.0 / 7.0;
            if (highLatitudeRule == HighLatitudeRule.NightMiddle)
                portion = 1.0 / 2.0;

            return portion * night;
        }

        public double AdjustTimeForHighLatitutes(double time, double bases, double angle, double night, HighLatitudeRule highLatitudeRule, bool isCcwDirection = false)
        {
            var portion = NightPortion(angle, night, highLatitudeRule);
            var timeDiff = isCcwDirection
                ? TimeUtilities.TimeDiff(time, bases)
                : TimeUtilities.TimeDiff(bases, time);
            if (double.IsNaN(time) || timeDiff > portion)
                time = bases + (isCcwDirection ? -portion : portion);
            return time;
        }
        private static double FixAngle(double angle)
        {
            angle = angle - 360 * (Math.Floor(angle / 360));
            return (angle < 0) ? angle + 360 : angle;
        }

        public double RiseSetAngle(double elevation)
        {
            var angle = 0.0347 * Math.Sqrt(elevation); // an approximation
            return 0.833 + angle;
        }
    }
}
