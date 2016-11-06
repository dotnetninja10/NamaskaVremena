using System;
namespace PrayerTimes.Types
{
    public class CalculationParameters
    {
        public double FajrAngle { get; private set; }
        public double MaghribAngle { get; private set; }
        public double IshaAngle { get; private set; }
        public int IshaIntervalAfterMaghrib { get; private set; }
        public AsrMethodMadhab AsrMethodCalculation { get; private set; }
        public HighLatitudeRule HighLatituteRule { get; private set; }
        public MidnightMethod MidnightMethod { get; private set; }
        public PrayerTimeAdjustments PrayerTimeAdjustments { get; private set; }
        public static CalculationMethods SelectedCalculationMethod { get; private set; }

        public CalculationParameters(double fajrAngle, double ishaAngle)
        {
            this.FajrAngle = fajrAngle;
            this.IshaAngle = ishaAngle;
            this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
        }
        public CalculationParameters(double fajrAngle, int ishaIntervalAfterMaghrib)
        {
            this.FajrAngle = fajrAngle;
            this.IshaIntervalAfterMaghrib = ishaIntervalAfterMaghrib;
            this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
        }

        public CalculationParameters(double fajrAngle, double maghribAngle, double ishaAngle) : this(fajrAngle, ishaAngle)
        {
            this.MaghribAngle = maghribAngle;
        }

        public CalculationParameters(double fajrAngle, double ishaAngle, PrayerTimeAdjustments timeAdjustments)
            : this(fajrAngle, ishaAngle)
        {
            this.PrayerTimeAdjustments = timeAdjustments;
        }
        public CalculationParameters(double fajrAngle, int ishaIntervalAfterMaghrib, PrayerTimeAdjustments timeAdjustments)
            : this(fajrAngle, ishaIntervalAfterMaghrib)
        {
            this.PrayerTimeAdjustments = timeAdjustments;
        }

        public void SetMidnightMethod(MidnightMethod method)
        {
            this.MidnightMethod = method;
        }
        public void SetHighLatituteRule(HighLatitudeRule method)
        {
            this.HighLatituteRule = method;
        }
        public void SetAsrMethodCalculation(AsrMethodMadhab method)
        {
            this.AsrMethodCalculation = method;
        }
        public static CalculationParameters SetCalculationMethod(CalculationMethods method)
        {
            SelectedCalculationMethod = method;

            switch (method)
            {
                case CalculationMethods.MUSLIM_WORLD_LEAGUE:
                    {
                        return new CalculationParameters(18.0, 17.0);
                    }
                case CalculationMethods.EGYPTIAN:
                    {
                        return new CalculationParameters(20.0, 18.0);
                    }
                case CalculationMethods.KARACHI:
                    {
                        return new CalculationParameters(18.0, 18.0);
                    }
                case CalculationMethods.UMM_AL_QURA:
                    {
                        return new CalculationParameters(18.5, 90);
                    }
                case CalculationMethods.GULF:
                    {
                        return new CalculationParameters(19.5, 90);
                    }
                case CalculationMethods.MOON_SIGHTING_COMMITTEE:
                    {
                        var c = new CalculationParameters(18.0, 18.0, new PrayerTimeAdjustments(-10, 0, 5, 0, 3, 0));
                        c.SetHighLatituteRule(HighLatitudeRule.OneSeventh);
                        return c;
                    }
                case CalculationMethods.NORTH_AMERICA:
                    {
                        return new CalculationParameters(15.0, 15.0);
                    }
                case CalculationMethods.KUWAIT:
                    {
                        return new CalculationParameters(18.0, 17.5);
                    }
                case CalculationMethods.QATAR:
                    {
                        return new CalculationParameters(18.0, 90);
                    }
                case CalculationMethods.SARAJEVO:
                    {
                        return new CalculationParameters(19.0, 17.0, new PrayerTimeAdjustments(-10, -3, 2, 0, 3, 0));
                    }
                case CalculationMethods.JAFARI:
                    {
                        return new CalculationParameters(16.0, 4.0, 14.0);
                    }
                case CalculationMethods.TEHERAN:
                    {
                        return new CalculationParameters(17.7, 4.5, 14.0);
                    }

                case CalculationMethods.OTHER:
                    {
                        return new CalculationParameters(0.0, 0.0);
                    }
                default:
                    {
                        throw new ArgumentException("Invalid CalculationMethod");
                    }
            }
        }
    }
}
