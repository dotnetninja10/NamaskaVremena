using System;

namespace PrayerTimes.Utilities
{
    public class MoonsightCalculationMethod
    {
        public double CalculateIshaMinimumGeneral(double lt, DateTime dateTime)
        {
            double A = 75 + (25.6 / 55.0) * Math.Abs(lt);
            double B = 75 + (2.05 / 55.0) * Math.Abs(lt);
            double C = 75 - (9.21 / 55.0) * Math.Abs(lt);
            double D = 75 + (6.14 / 55.0) * Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
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

        public double CalculateIshaMinimumAbyad(double lt, DateTime dateTime)
        {
            var A = 75 + (25.6 / 55) * Math.Abs(lt);
            var B = 75 + (7.16 / 55) * Math.Abs(lt);
            var C = 75 + (36.84 / 55) * Math.Abs(lt);
            var D = 75 + (81.84 / 55) * Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
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

        public double CalculateFarjMinimumGeneral(double lt, DateTime dateTime)
        {
            var A = 75 + (28.65 / 55) * Math.Abs(lt);
            var B = 75 + (19.44 / 55) * Math.Abs(lt);
            var C = 75 + (32.74 / 55) * Math.Abs(lt);
            var D = 75 + (48.1 / 55) * Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);

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

        public double CalculateIshaMinimumAhmer(double lt, DateTime dateTime)
        {
            var A = 62 + (17.4 / 55) * Math.Abs(lt);
            var B = 62 - (7.16 / 55) * Math.Abs(lt);
            var C = 62 + (5.12 / 55) * Math.Abs(lt);
            var D = 62 + (19.44 / 55) * Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
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

        private static int DaysSinceSolstice(int dayOfYear, int year, double latitude)
        {
            int daysSinceSolistice;
            const int northernOffset = 10;
            var isLeapYear = DateTime.IsLeapYear(year);
            var southernOffset = isLeapYear ? 173 : 172;
            var daysInYear = isLeapYear ? 366 : 365;

            if (latitude >= 0)
            {
                daysSinceSolistice = dayOfYear + northernOffset;
                if (daysSinceSolistice >= daysInYear)
                {
                    daysSinceSolistice = daysSinceSolistice - daysInYear;
                }
            }
            else
            {
                daysSinceSolistice = dayOfYear - southernOffset;
                if (daysSinceSolistice < 0)
                {
                    daysSinceSolistice = daysSinceSolistice + daysInYear;
                }
            }
            return daysSinceSolistice;
        }
    }
}
