using System;

namespace PrayerTimes.Utilities
{
    public static class MathUtilities
    {
        public static double Sin(double degree)
        {
            return Math.Sin(DegreeToRadian(degree));
        }

        public static double Cos(double degree)
        {
            return Math.Cos(DegreeToRadian(degree));
        }

        public static double Tan(double degree)
        {
            return Math.Tan(DegreeToRadian(degree));
        }

        public static double ArcSin(double degree)
        {
            return RadianToDegree(Math.Asin(degree));
        }

        public static double ArcCos(double degree)
        {
            return RadianToDegree(Math.Acos(degree));
        }

        public static double ArcTan(double degree)
        {
            return RadianToDegree(Math.Atan(degree));

        }
        public static double ArcCot(double degree)
        {
            return RadianToDegree(Math.Atan(1 / degree));
        }

        public static double ArcTan2(double y, double x)
        {
            return RadianToDegree(Math.Atan2(y, x));
        }

        private static double DegreeToRadian(double degree)
        {
            return (degree * Math.PI) / 180.0;
        }

        private static double RadianToDegree(double radian)
        {
            return (radian * 180.0) / Math.PI;
        }

    }
}

