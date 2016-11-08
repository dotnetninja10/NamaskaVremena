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
        public CalculationMethods SelectedCalculationMethod { get; private set; }
        public CalculationParameters(CalculationMethods calculationMethod, 
            AsrMethodMadhab asrMethod, 
            HighLatitudeRule highLatituteRule, 
            MidnightMethod midnightMethod, 
            PrayerTimeAdjustments prayerTimeAdjustments) : this(calculationMethod)
        {
            this.AsrMethodCalculation = asrMethod;
            this.HighLatituteRule = highLatituteRule;
            this.MidnightMethod = midnightMethod;
            this.PrayerTimeAdjustments = prayerTimeAdjustments;
        }
        public CalculationParameters(double fajrAngle, double ishaAngle,
           AsrMethodMadhab asrMethod,
           HighLatitudeRule highLatituteRule,
           MidnightMethod midnightMethod,
           PrayerTimeAdjustments prayerTimeAdjustments)
        {
            this.FajrAngle = fajrAngle;
            this.IshaAngle = ishaAngle;
            this.AsrMethodCalculation = asrMethod;
            this.HighLatituteRule = highLatituteRule;
            this.MidnightMethod = midnightMethod;
            this.PrayerTimeAdjustments = prayerTimeAdjustments;
        }

        public CalculationParameters(CalculationMethods calculationMethod)
        {
            this.SelectedCalculationMethod = calculationMethod;
            this.HighLatituteRule = HighLatitudeRule.OneSeventh;
            this.AsrMethodCalculation = AsrMethodMadhab.Shafii;
            this.MidnightMethod=MidnightMethod.Standard;
            this.PrayerTimeAdjustments= new PrayerTimeAdjustments();

            switch (calculationMethod)
            {
                case CalculationMethods.MUSLIM_WORLD_LEAGUE:
                {
                    this.FajrAngle = 18.0;
                    this.IshaAngle = 17.0;
                    break;
                }
                case CalculationMethods.EGYPTIAN:
                {
                    this.FajrAngle = 20.0;
                    this.IshaAngle = 18.0;
                    break;
                }
                case CalculationMethods.KARACHI:
                {
                    this.FajrAngle = 18.0;
                    this.IshaAngle = 18.0;
                    break;
                }
                case CalculationMethods.UMM_AL_QURA:
                {
                    this.FajrAngle = 18.5;
                    this.IshaIntervalAfterMaghrib = 90;
                    break;
                }
                case CalculationMethods.GULF:
                {
                    this.FajrAngle = 19.5;
                    this.IshaIntervalAfterMaghrib = 90;
                    break;
                }
                case CalculationMethods.MOON_SIGHTING_COMMITTEE:
                {
                    this.FajrAngle = 18.0;
                    this.IshaAngle = 18.0;
                    this.HighLatituteRule = HighLatitudeRule.OneSeventh;
                    this.PrayerTimeAdjustments = new PrayerTimeAdjustments(-10, 0, 5, 0, 3, 0);
                    break;
                }
                case CalculationMethods.NORTH_AMERICA:
                {
                    this.FajrAngle = 15.0;
                    this.IshaAngle = 15.0;
                    break;
                }
                case CalculationMethods.KUWAIT:
                {
                    this.FajrAngle = 18.0;
                    this.IshaAngle = 17.5;
                    break;
                }
                case CalculationMethods.QATAR:
                {
                    this.FajrAngle = 18.0;
                    this.IshaIntervalAfterMaghrib = 90;
                    break;
                }
                case CalculationMethods.SARAJEVO:
                {
                    this.FajrAngle = 19.0;
                    this.IshaAngle = 17.0;
                    this.HighLatituteRule = HighLatitudeRule.OneSeventh;
                    this.PrayerTimeAdjustments = new PrayerTimeAdjustments(-10, -3, 2, 0, 3, 0);
                    break;
                }
                case CalculationMethods.JAFARI:
                {
                    this.FajrAngle = 16.0;
                    this.MaghribAngle = 4.0;
                    this.IshaAngle = 14.0;
                    this.MidnightMethod = MidnightMethod.Jafari;
                    break;
                }
                case CalculationMethods.TEHERAN:
                {
                    this.FajrAngle = 17.7;
                    this.MaghribAngle = 4.5;
                    this.IshaAngle = 14.0;
                    this.MidnightMethod = MidnightMethod.Jafari;
                   break;
                }

                case CalculationMethods.OTHER:
                {
                    this.FajrAngle = 10.0;
                    this.IshaAngle = 10.0;
                    break;
                }
                default:
                {
                    throw new ArgumentException("Invalid CalculationMethod");
                }
            }
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
        //public static CalculationParameters SetCalculationMethod(CalculationMethods method)
        //{
        //    SelectedCalculationMethod = method;

        //    switch (method)
        //    {
        //        case CalculationMethods.MUSLIM_WORLD_LEAGUE:
        //            {
        //                return new CalculationParameters(18.0, 17.0);
        //            }
        //        case CalculationMethods.EGYPTIAN:
        //            {
        //                return new CalculationParameters(20.0, 18.0);
        //            }
        //        case CalculationMethods.KARACHI:
        //            {
        //                return new CalculationParameters(18.0, 18.0);
        //            }
        //        case CalculationMethods.UMM_AL_QURA:
        //            {
        //                return new CalculationParameters(18.5, 90);
        //            }
        //        case CalculationMethods.GULF:
        //            {
        //                return new CalculationParameters(19.5, 90);
        //            }
        //        case CalculationMethods.MOON_SIGHTING_COMMITTEE:
        //            {
        //                var c = new CalculationParameters(18.0, 18.0, new PrayerTimeAdjustments(-10, 0, 5, 0, 3, 0))
        //                {
        //                    HighLatituteRule = HighLatitudeRule.OneSeventh
        //                };
        //                return c;
        //            }
        //        case CalculationMethods.NORTH_AMERICA:
        //            {
        //                return new CalculationParameters(15.0, 15.0);
        //            }
        //        case CalculationMethods.KUWAIT:
        //            {
        //                return new CalculationParameters(18.0, 17.5);
        //            }
        //        case CalculationMethods.QATAR:
        //            {
        //                return new CalculationParameters(18.0, 90);
        //            }
        //        case CalculationMethods.SARAJEVO:
        //            {
        //                return new CalculationParameters(19.0, 17.0, new PrayerTimeAdjustments(-10, -3, 2, 0, 3, 0));
        //            }
        //        case CalculationMethods.JAFARI:
        //            {
        //                return new CalculationParameters(16.0, 4.0, 14.0);
        //            }
        //        case CalculationMethods.TEHERAN:
        //            {
        //                var c = new CalculationParameters(17.7, 4.5, 14.0)
        //                {
        //                    MidnightMethod = MidnightMethod.Jafari,
        //                    HighLatituteRule = HighLatitudeRule.OneSeventh
        //                };
        //                return c;
        //            }

        //        case CalculationMethods.OTHER:
        //            {
        //                return new CalculationParameters(0.0, 0.0);
        //            }
        //        default:
        //            {
        //                throw new ArgumentException("Invalid CalculationMethod");
        //            }
        //    }
        //}
    }
}
