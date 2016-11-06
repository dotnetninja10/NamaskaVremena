//using System;
//using System.CodeDom;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.UI.HtmlControls;
//using SalahTimes.Controllers;
//using SalahTimes.Domain;

//namespace SalahTimes.Experiments
//{



//    //public enum AsrMethodMadhab
//    //{
//    //    Shafii = 1, //Shadow lenght
//    //    Hanafi = 2 //Double shadow length
//    //}
//    //public enum HighLatitudeRule
//    //{
//    //    None,
//    //    NightMiddle,
//    //    AngleBased,
//    //    OneSeventh
//    //}

//    //public enum MidnightMethod
//    //{
//    //    Standard, // Mid Sunset to Sunrise
//    //    Jafari // Mid Sunset to Fajr--Shia
//    //}

//    //public enum TimeFormats
//    //{
//    //    Hour24,
//    //    Hour12,
//    //    Hour12hNS,
//    //    Hfloat
//    //}
//    public enum CalculationMethods
//    {
//        /**
//         * Muslim World League
//         * Uses Fajr angle of 18 and an Isha angle of 17
//         */
//        MUSLIM_WORLD_LEAGUE,

//        /**
//         * Egyptian General Authority of Survey
//         * Uses Fajr angle of 19.5 and an Isha angle of 17.5
//         */
//        EGYPTIAN,

//        /**
//         * University of Islamic Sciences, Karachi
//         * Uses Fajr angle of 18 and an Isha angle of 18
//         */
//        KARACHI,

//        /**
//         * Umm al-Qura University, Makkah
//         * Uses a Fajr angle of 18.5 and an Isha angle of 90. Note: You should add a +30 minute custom
//         * adjustment of Isha during Ramadan.
//         */
//        UMM_AL_QURA,

//        /**
//         * The Gulf Region
//         * Modified version of Umm al-Qura that uses a Fajr angle of 19.5.
//         */
//        GULF,

//        /**
//         * Moonsighting Committee
//         * Uses a Fajr angle of 18 and an Isha angle of 18. Also uses seasonal adjustment values.
//         */
//        MOON_SIGHTING_COMMITTEE,

//        /**
//         * Referred to as the ISNA method
//         * This method is included for completeness, but is not recommended.
//         * Uses a Fajr angle of 15 and an Isha angle of 15.
//         */
//        NORTH_AMERICA,

//        /**
//         * Kuwait
//         * Uses a Fajr angle of 18 and an Isha angle of 17.5
//         */
//        KUWAIT,

//        /**
//         * Qatar
//         * Modified version of Umm al-Qura that uses a Fajr angle of 18.
//         */
//        QATAR,

//        /**
//         * The default value for {@link CalculationParameters#method} when initializing a
//         * {@link CalculationParameters} object. Sets a Fajr angle of 0 and an Isha angle of 0.
//         */
//        OTHER

//    }
//    //public class PrayerTimeAdjustments
//    //{
//    //    public short Imsak = 0; //default 10 minutes of imsak before Fajr
//    //    public short Fajr = 0;
//    //    public short Dhuhr = 0;
//    //    public short Asr = 0;
//    //    public short Maghrib = 0;
//    //    public short Isha = 0;

//    //    public PrayerTimeAdjustments()
//    //    {
//    //        this.Imsak = 10;
//    //        this.Fajr = 0;
//    //        this.Dhuhr = 0;
//    //        this.Asr = 0;
//    //        this.Maghrib = 0;
//    //        this.Isha = 0;
//    //    }
//    //    public PrayerTimeAdjustments(short imsak, short fajr, short dhuhr, short asr, short maghrib, short isha)
//    //    {
//    //        this.Imsak = imsak;
//    //        this.Fajr = fajr;
//    //        this.Dhuhr = dhuhr;
//    //        this.Asr = asr;
//    //        this.Maghrib = maghrib;
//    //        this.Isha = isha;
//    //    }
//    //}

//    //public class PrayerTimes
//    //{
//    //    public double Imsak;
//    //    public double Fajr;
//    //    public double Sunrise;
//    //    public double Dhuhr;
//    //    public double Asr;
//    //    public double Sunset;
//    //    public double Maghrib;
//    //    public double Isha;
//    //    public double Midnight;

//    //    public PrayerTimes(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
//    //        double defaultAsr, double defaultSunset, double defaultMaghrib, double defaultIsha,
//    //        double defaultMidnight)
//    //    {
//    //        Imsak = defaultImsak;
//    //        Fajr = defaultFajr;
//    //        Sunrise = defaultSunrise;
//    //        Dhuhr = defaultDhur;
//    //        Asr = defaultAsr;
//    //        Sunset = defaultSunset;
//    //        Maghrib = defaultMaghrib;
//    //        Isha = defaultIsha;
//    //        Midnight = defaultMidnight;
//    //    }

//    //    //private PrayerTimes _dayPortion;
//    //    //private PrayerTimes DayPortion => _dayPortion ?? 
//    //    //                                    (_dayPortion = new PrayerTimes(this.Imsak/24,
//    //    //                                                        this.Fajr/24,
//    //    //                                                        this.Sunrise/24,
//    //    //                                                        this.Dhuhr/24,
//    //    //                                                        this.Asr/24,
//    //    //                                                        this.Sunset/24,
//    //    //                                                        this.Maghrib/24,
//    //    //                                                        this.Isha/24,
//    //    //                                                        this.Midnight));
//    //}
//    //public class Coordinates
//    //{
//    //    public double Latitude { get; private set; }
//    //    public double Longtitud { get; private set; }

//    //    public double Elevation { get; private set; }

//    //    public Coordinates(double lat, double lon)
//    //    {
//    //        Latitude = lat;
//    //        Longtitud = lon;
//    //        Elevation = 0;
//    //    }
//    //    public Coordinates(double lat, double lon, int elevation) : this(lat, lon)
//    //    {
//    //        Elevation = elevation;
//    //    }
//    //}

//    //public class CalculationParameters
//    //{
//    //    public double FajrAngle { get; private set; }
//    //    public double MaghribAngle { get; private set; }
//    //    public double IshaAngle { get; private set; }
//    //    public int IshaIntervalAfterMaghrib { get; private set; }
//    //    public AsrMethodMadhab AsrMethodCalculation { get; private set; }
//    //    public HighLatitudeRule HighLatituteRule { get; private set; }
//    //    public MidnightMethod MidnightMethod { get; private set; }
//    //    public PrayerTimeAdjustments PrayerTimeAdjustments { get; private set; }

//    //    public CalculationParameters(double fajrAngle, double ishaAngle)
//    //    {
//    //        this.FajrAngle = fajrAngle;
//    //        this.IshaAngle = ishaAngle;
//    //        this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
//    //    }

//    //    public CalculationParameters(double fajrAngle, int ishaIntervalAfterMaghrib)
//    //    {
//    //        this.FajrAngle = fajrAngle;
//    //        this.IshaIntervalAfterMaghrib = ishaIntervalAfterMaghrib;
//    //        this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
//    //    }

//    //    public CalculationParameters(double fajrAngle, double ishaAngle, PrayerTimeAdjustments timeAdjustments)
//    //        : this(fajrAngle, ishaAngle)
//    //    {
//    //        this.PrayerTimeAdjustments = timeAdjustments;
//    //    }

//    //    public CalculationParameters GetParameters(CalculationMethods method)
//    //    {
//    //        switch (method)
//    //        {
//    //            case CalculationMethods.MUSLIM_WORLD_LEAGUE:
//    //                {
//    //                    return new CalculationParameters(18.0, 17.0);
//    //                }
//    //            case CalculationMethods.EGYPTIAN:
//    //                {
//    //                    return new CalculationParameters(20.0, 18.0);
//    //                }
//    //            case CalculationMethods.KARACHI:
//    //                {
//    //                    return new CalculationParameters(18.0, 18.0);
//    //                }
//    //            case CalculationMethods.UMM_AL_QURA:
//    //                {
//    //                    return new CalculationParameters(18.5, 90);
//    //                }
//    //            case CalculationMethods.GULF:
//    //                {
//    //                    return new CalculationParameters(19.5, 90);
//    //                }
//    //            case CalculationMethods.MOON_SIGHTING_COMMITTEE:
//    //                {
//    //                    return new CalculationParameters(18.0, 18.0);
//    //                }
//    //            case CalculationMethods.NORTH_AMERICA:
//    //                {
//    //                    return new CalculationParameters(15.0, 15.0);
//    //                }
//    //            case CalculationMethods.KUWAIT:
//    //                {
//    //                    return new CalculationParameters(18.0, 17.5);
//    //                }
//    //            case CalculationMethods.QATAR:
//    //                {
//    //                    return new CalculationParameters(18.0, 90);
//    //                }
//    //            case CalculationMethods.OTHER:
//    //                {
//    //                    return new CalculationParameters(0.0, 0.0);
//    //                }
//    //            default:
//    //                {
//    //                    throw new ArgumentException("Invalid CalculationMethod");
//    //                }
//    //        }
//    //    }
//    //}
//    //public class DateComponent
//    //{
//    //    public int TimeZone { get; private set; }
//    //    public DateTime CalculationDate { get; private set; }
//    //    //JUlianDate
//    //    public double JulianDate { get; private set; }

//    //    public DateComponent(DateTime calculationDate, int timeZone)
//    //    {
//    //        this.CalculationDate = calculationDate;
//    //        this.TimeZone = timeZone;
//    //        this.JulianDate = ToJulianDate(calculationDate.Year, calculationDate.Month, calculationDate.Day);
//    //    }
//    //    private double ToJulianDate(int year, int month, int day)
//    //    {
//    //        if (month <= 2)
//    //        {
//    //            year -= 1;
//    //            month += 12;
//    //        }
//    //        var A = (double)Math.Floor(year / 100.0);
//    //        var B = 2 - A + Math.Floor(A / 4);

//    //        var JD = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
//    //        return JD;
//    //    }

//    //}

//    //public class MathUtilities
//    //{
//    //    public static double SinRadian(double degree)
//    //    {
//    //        return Math.Sin(DegreeToRadian(degree));
//    //    }

//    //    public static double CosRadian(double degree)
//    //    {
//    //        return Math.Cos(DegreeToRadian(degree));
//    //    }
//    //    public static double TanRadian(double degree)
//    //    {
//    //        return Math.Tan(DegreeToRadian(degree));
//    //    }

//    //    public static double ArcSin(double degree)
//    //    {
//    //        return RadianToDegree(Math.Asin(degree));
//    //    }

//    //    public static double ArcCos(double degree)
//    //    {
//    //        return RadianToDegree(Math.Acos(degree));
//    //    }

//    //    public static double ArcTan(double degree)
//    //    {
//    //        return RadianToDegree(Math.Atan(degree));

//    //    }

//    //    public static double ArcCot(double degree)
//    //    {
//    //        return RadianToDegree(Math.Atan(1 / degree));
//    //    }

//    //    public static double ArcTan2(double y, double x)
//    //    {
//    //        return RadianToDegree(Math.Atan2(y, x));
//    //    }

//    //    private static double DegreeToRadian(double degree)
//    //    {
//    //        return (degree * Math.PI) / 180.0;
//    //    }

//    //    private static double RadianToDegree(double radian)
//    //    {
//    //        return (radian * 180.0) / Math.PI;
//    //    }
//    //}
//    //public class SunTimes
//    //{
//    //    public static double SunAngleTime(double angle, double time, double lat, string direction = "NODIR")
//    //    {
//    //        var decl = sunPosition(JDate + time).declination;
//    //        var noon = MidDay(time);
//    //        var t = (1 / 15.0) *
//    //                MathUtilities.ArcCos((-MathUtilities.SinRadian(angle) - MathUtilities.SinRadian(decl) * MathUtilities.SinRadian(lat)) /
//    //                (MathUtilities.CosRadian(decl) * MathUtilities.CosRadian(lat)));
//    //        return noon + (direction == CCW ? -t : t);
//    //    }

//    //    private static sunAngle sunPosition(double jd)
//    //    {
//    //        double D = jd - 2451545.0;
//    //        double g = FixAngle(357.529 + 0.98560028 * D);
//    //        double q = FixAngle(280.459 + 0.98564736 * D);
//    //        double L = FixAngle(q + 1.915 * MathUtilities.ArcSin(g) + 0.020 * MathUtilities.ArcSin(2 * g));

//    //        double R = 1.00014 - 0.01671 * MathUtilities.CosRadian(g) - 0.00014 * MathUtilities.CosRadian(2 * g);
//    //        double e = 23.439 - 0.00000036 * D;
//    //        var RA = MathUtilities.ArcTan2(MathUtilities.CosRadian(e) * MathUtilities.SinRadian(L), MathUtilities.CosRadian(L)) / 15;
//    //        var eqt = q / 15 - FixHour(RA);
//    //        var decl = MathUtilities.ArcSin(MathUtilities.SinRadian(e) * MathUtilities.SinRadian(L));

//    //        return new sunAngle(decl, eqt);

//    //    }

//    //    struct sunAngle

//    //    {
//    //        public readonly double declination;
//    //        public readonly double equation;

//    //        public sunAngle(double d, double e)
//    //        {
//    //            declination = d;
//    //            equation = e;
//    //        }
//    //    }
//    //    public static double TimeDiff(double time1, double time2)
//    //    {
//    //        return FixHour(time2 - time1);
//    //    }

//    //    public static double AsrTime(int factor, double time,double lat)
//    //    {
//    //        var decl = sunPosition(JDate + time).declination;
//    //        var angle = -MathUtilities.ArcCot(factor + MathUtilities.TanRadian(Math.Abs(lat - decl)));
//    //        return SunAngleTime(angle, time,lat);
//    //    }

//    //    public static double MidDay(double time)
//    //    {
//    //        var eqt = sunPosition(JDate + time).equation;
//    //        var noon = FixHour(12 - eqt);
//    //        return noon;
//    //    }
//    //    private static double FixAngle(double angle)
//    //    {
//    //        return Fix(angle, 360);
//    //    }

//    //    private static double FixHour(double a)
//    //    {
//    //        return Fix(a, 24);
//    //    }
//    //    private static double Fix(double a, int b)
//    //    {
//    //        a = a - b * (Math.Floor(a / b));
//    //        return (a < 0) ? a + b : a;
//    //    }
//    //    public static double RiseSetAngle(double elevation)
//    //    {
//    //        var angle = 0.0347 * Math.Sqrt(elevation); // an approximation
//    //        return 0.833 + angle;
//    //    }

//    //}

//    //public class PrayerCalc2
//    //{

//    //    #region"Properties"

//    //    private Coordinates Coordinate { get; set; }
//    //    private DateComponent DateComponent { get; set; }
//    //    private CalculationParameters Parameters { get; set; }

//    //    private short _numberOfIterations = 1;

//    //    private PrayerTimes _prayerTimes = null;
//    //    #endregion
//    //    public string[] CalculateTimes(Coordinates coordinates, DateComponent dateComponent, CalculationParameters parameters)
//    //    {
//    //        this.Coordinate = coordinates;
//    //        this.DateComponent = dateComponent;
//    //        this.Parameters = parameters;

//    //        return this.ProcessPrayerTimes();
//    //    }
//    //    private string[] ProcessPrayerTimes()
//    //    {
//    //        _prayerTimes = new PrayerTimes(5, 5, 6, 12, 13, 18, 18, 18, 18);//default values

//    //        for (int i = 0; i < this._numberOfIterations; i++)
//    //        {
//    //            _prayerTimes = this.ComputePrayerTimes(_prayerTimes);
//    //        }

//    //        _prayerTimes = this.adjustTimes(_prayerTimes);

//    //        _prayerTimes.Midnight = (Parameters.MidnightMethod == MidnightMethod.Jafari)
//    //            ? _prayerTimes.Sunset + SunTimes.TimeDiff(_prayerTimes.Sunset, _prayerTimes.Fajr) / 2
//    //            : _prayerTimes.Sunset + SunTimes.TimeDiff(_prayerTimes.Sunset, _prayerTimes.Sunrise) / 2;

//    //        _prayerTimes = this.tuneTimes(_prayerTimes);
//    //        return this.adjustTimesFormat(_prayerTimes);
//    //    }
//    //    private PrayerTimes ComputePrayerTimes(PrayerTimes times)
//    //    {
//    //        times = this.DayPortion(times);
//    //        var parameters = this.Parameters;

//    //        var sunrise = SunTimes.SunAngleTime(SunTimes.RiseSetAngle(Coordinate.Elevation), times.Sunrise, Coordinate.Latitude, CCW);
//    //        var sunset = SunTimes.SunAngleTime(SunTimes.RiseSetAngle(Coordinate.Elevation), times.Sunset, Coordinate.Latitude);

//    //        var fajr = _useMoonSight ? MoonSightFarj(sunrise) : SunTimes.SunAngleTime(parameters.FajrAngle, times.Fajr, Coordinate.Latitude, CCW);
//    //        var dhuhr = SunTimes.MidDay(times.Dhuhr);
//    //        var asr = SunTimes.AsrTime((int)parameters.AsrMethodCalculation, times.Asr, Coordinate.Latitude);
//    //        var maghrib = SunTimes.SunAngleTime(parameters.MaghribAngle, times.Maghrib, Coordinate.Latitude);
//    //        var isha = _useMoonSight ? MoonSightIsha(sunset) : SunTimes.SunAngleTime(parameters.IshaAngle, times.Isha, Coordinate.Latitude);

//    //        return new PrayerTimes(times.Imsak, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha, 0.0);
//    //    }

//    //    private PrayerTimes adjustTimes(PrayerTimes times)
//    //    {

//    //        times.Imsak += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Fajr += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Sunrise += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Dhuhr += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Asr += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Sunset += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Maghrib += DateComponent.TimeZone - Coordinate.Longtitud / 15;
//    //        times.Isha += DateComponent.TimeZone - Coordinate.Longtitud / 15;

//    //        if (Parameters.HighLatituteRule != HighLatitudeRule.None)
//    //            times = this.adjustHighLats(times);

//    //        //if (parameters.imsakMinutes >= 0)
//    //            times.Imsak = times.Fajr - Parameters.PrayerTimeAdjustments.Imsak / 60.0;
//    //        if (parameters.maghribMinutes >= 0 && Math.Abs(parameters.maghribAngle) < 0.001)
//    //            times.Maghrib = times.Sunset + parameters.maghribMinutes / 60.0;
//    //        if (parameters.ishaMinutes > 0)
//    //            times.Isha = times.Maghrib + parameters.ishaAngle / 60.0;
//    //        times.Dhuhr += parameters.dhuhr / 60.0;
//    //        return times;


//    //    }
//    //    private PrayerTimes adjustHighLats(PrayerTimes times)
//    //    {
//    //        var parameters = Parameters;
//    //        var nightTime = SunTimes.TimeDiff(times.Sunset, times.Sunrise);

//    //        times.Imsak = this.adjustHLTime(times.Imsak, times.Sunrise, parameters.ImsakMinutes, nightTime, CCW);
//    //        times.Fajr = this.adjustHLTime(times.Fajr, times.Sunrise, parameters.fajrAngle, nightTime, CCW);
//    //        times.Isha = this.adjustHLTime(times.Isha, times.Sunset, parameters.ishaAngle, nightTime);
//    //        times.Maghrib = this.adjustHLTime(times.Isha, times.Sunset, parameters.maghribMinutes, nightTime);

//    //        return times;
//    //    }
//    //    private double adjustHLTime(double time, double bases, double angle, double night, string direction = "NONE")
//    //    {
//    //        var portion = this.nightPortion(angle, night);
//    //        var timeDiff = (direction == CCW)
//    //            ? this.timeDiff(time, bases)
//    //            : this.timeDiff(bases, time);
//    //        if (double.IsNaN(time) || timeDiff > portion)
//    //            time = bases + (direction == CCW ? -portion : portion);
//    //        return time;
//    //    }
//    //    private PrayerTimes DayPortion(PrayerTimes times)
//    //    {
//    //        times.Imsak /= 24;
//    //        times.Imsak /= 24;
//    //        times.Fajr /= 24;
//    //        times.Sunrise /= 24;
//    //        times.Dhuhr /= 24;
//    //        times.Asr /= 24;
//    //        times.Sunset /= 24;
//    //        times.Maghrib /= 24;
//    //        times.Isha /= 24;

//    //        return times;
//    //    }

//    //}

//}
//namespace SalahTimes.Domain
//{
//    //public class PrayerCalc
//    //{
//    //    enum AsrMethodMadhab
//    //    {
//    //        Shafii = 1, //Shadow lenght
//    //        Hanafi = 2 //Double shadow length
//    //    }

//    //    enum HighLatitudeRule
//    //    {
//    //        None,
//    //        NightMiddle,
//    //        AngleBased,
//    //        OneSeventh
//    //    }

//    //    enum MidnightMethod
//    //    {
//    //        Standard, // Mid Sunset to Sunrise
//    //        Jafari // Mid Sunset to Fajr--Shia
//    //    }

//    //    public class PrayerTimeAdjustments
//    //    {
//    //        private short Imsak = 10; //default 10 minutes of imsak before Fajr
//    //        private short Fajr = 0;
//    //        private short Dhuhr = 0;
//    //        private short Asr = 0;
//    //        private short Maghrib = 0;
//    //        private short Isha = 0;

//    //        public PrayerTimeAdjustments(short imsak, short fajr, short dhuhr, short asr, short maghrib, short isha)
//    //        {
//    //            this.Imsak = imsak;
//    //            this.Fajr = fajr;
//    //            this.Dhuhr = dhuhr;
//    //            this.Asr = asr;
//    //            this.Maghrib = maghrib;
//    //            this.Isha = isha;
//    //        }
//    //    }

//    //    public class CalculationParameter
//    //    {
//    //        double FajrAngle;
//    //        double MaghribAngle;
//    //        double IshaAngle;
//    //        int IshaIntervalAfterMaghrib;
//    //        AsrMethodMadhab AsrMethodCalculation;
//    //        HighLatitudeRule HighLatituteRule;
//    //        MidnightMethod MidnightMethod;
//    //        PrayerTimeAdjustments PrayerTimeAdjustments;

//    //        public CalculationParameter(double fajrAngle, double ishaAngle)
//    //        {
//    //            this.FajrAngle = fajrAngle;
//    //            this.IshaAngle = ishaAngle;
//    //        }

//    //        public CalculationParameter(double fajrAngle, int ishaIntervalAfterMaghrib)
//    //        {
//    //            this.FajrAngle = fajrAngle;
//    //            this.IshaIntervalAfterMaghrib = ishaIntervalAfterMaghrib;
//    //        }

//    //        public CalculationParameter(double fajrAngle, double ishaAngle, PrayerTimeAdjustments timeAdjustments)
//    //            : this(fajrAngle, ishaAngle)
//    //        {
//    //            this.PrayerTimeAdjustments = timeAdjustments;
//    //        }
//    //    }

//    //    public class PrayerTimes
//    //    {
//    //        double Imsak;
//    //        double Fajr;
//    //        double Sunrise;
//    //        double Dhuhr;
//    //        double Asr;
//    //        double Sunset;
//    //        double Maghrib;
//    //        double Isha;
//    //        double Midnight;

//    //        public PrayerTimes(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
//    //            double defaultAsr, double defaultSunset, double defaultMaghrib, double defaultIsha,
//    //            double defaultMidnight)
//    //        {
//    //            Imsak = defaultImsak;
//    //            Fajr = defaultFajr;
//    //            Sunrise = defaultSunrise;
//    //            Dhuhr = defaultDhur;
//    //            Asr = defaultAsr;
//    //            Sunset = defaultSunset;
//    //            Maghrib = defaultMaghrib;
//    //            Isha = defaultIsha;
//    //            Midnight = defaultMidnight;
//    //        }

//    //    }

//    //    public class Coordinates
//    //    {
//    //        public double Latitude { get; private set; }
//    //        public double Longtitud { get; private set; }

//    //        public double Elevation { get; private set; }

//    //        public Coordinates(double lat, double lon, int elevation)
//    //        {
//    //            Latitude = lat;
//    //            Longtitud = lon;
//    //            Elevation = elevation;
//    //        }
//    //    }

//    //    public class DateSettings
//    //    {
//    //        private int TimeZone { get; set; }
//    //        private DateTime CalculationDate { get; set; }
//    //        //JUlianDate
//    //        private double JDate { get; set; }

//    //        public DateSettings(DateTime calculationDate, int timeZone)
//    //        {

//    //            this.TimeZone = timeZone;
//    //        }

//    //    }

//    //    #region"Properties"

//    //    private Coordinates Coordinate { get; set; }

//    //    #endregion


//    //    public void CalculateTimes(DateTime date, Coordinates coordinates, int timeZone, int elevation)
//    //    {
//    //        this.Coordinate = coordinates;
//    //        //this.DateTimeFormat = date;
//    //        //this.lat = latitude;
//    //        //this.lng = longitude;
//    //        //this.elv = dst;
//    //        //this.timeZone = timeZoneC;
//    //        //this.JDate = this.julian(date.Year, date.Month, date.Day) - longitude / (15 * 24);

//    //        //return this.computeTimes1();
//    //    }
//    //}
//    public enum AsrMethodMadhab
//    {
//        Shafii = 0, //Shadow lenght
//        Hanafi = 1 //Double shadow length
//    }
//    public enum HighLatitudeRule
//    {
//        None,
//        NightMiddle,
//        AngleBased,
//        OneSeventh
//    }

//    public enum MidnightMethod
//    {
//        Standard, // Mid Sunset to Sunrise
//        Jafari // Mid Sunset to Fajr--Shia
//    }

//    public enum TimeFormats
//    {
//        Hour24,
//        Hour12,
//        Hour12hNS,
//        Hfloat
//    }
//    public class Coordinates
//    {
//        public double Latitude { get; private set; }
//        public double Longtitud { get; private set; }

//        public double Elevation { get; private set; }

//        public Coordinates(double lat, double lon)
//        {
//            Latitude = lat;
//            Longtitud = lon;
//            Elevation = 0;
//        }
//        public Coordinates(double lat, double lon, int elevation) : this(lat, lon)
//        {
//            Elevation = elevation;
//        }
//    }

//    public class DateComponent
//    {
//        public int TimeZone { get; private set; }
//        public DateTime CalculationDate { get; private set; }
//        //JUlianDate
//        //public double JulianDate { get; private set; }

//        public DateComponent(DateTime calculationDate, int timeZone)
//        {
//            this.CalculationDate = calculationDate;
//            this.TimeZone = timeZone;
//            //this.JulianDate = ToJulianDate(calculationDate.Year, calculationDate.Month, calculationDate.Day);
//        }
//        //private double ToJulianDate(int year, int month, int day)
//        //{
//        //    if (month <= 2)
//        //    {
//        //        year -= 1;
//        //        month += 12;
//        //    }
//        //    var A = (double)Math.Floor(year / 100.0);
//        //    var B = 2 - A + Math.Floor(A / 4.00);

//        //    var JD = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.500;
//        //    return JD;

//        //}

//    }
//    public enum CalculationMethods
//    {
//        /**
//         * Muslim World League
//         * Uses Fajr angle of 18 and an Isha angle of 17
//         */
//        MUSLIM_WORLD_LEAGUE,

//        /**
//         * Egyptian General Authority of Survey
//         * Uses Fajr angle of 19.5 and an Isha angle of 17.5
//         */
//        EGYPTIAN,

//        /**
//         * University of Islamic Sciences, Karachi
//         * Uses Fajr angle of 18 and an Isha angle of 18
//         */
//        KARACHI,

//        /**
//         * Umm al-Qura University, Makkah
//         * Uses a Fajr angle of 18.5 and an Isha angle of 90. Note: You should add a +30 minute custom
//         * adjustment of Isha during Ramadan.
//         */
//        UMM_AL_QURA,

//        /**
//         * The Gulf Region
//         * Modified version of Umm al-Qura that uses a Fajr angle of 19.5.
//         */
//        GULF,

//        /**
//         * Moonsighting Committee
//         * Uses a Fajr angle of 18 and an Isha angle of 18. Also uses seasonal adjustment values.
//         */
//        MOON_SIGHTING_COMMITTEE,

//        /**
//         * Referred to as the ISNA method
//         * This method is included for completeness, but is not recommended.
//         * Uses a Fajr angle of 15 and an Isha angle of 15.
//         */
//        NORTH_AMERICA,

//        /**
//         * Kuwait
//         * Uses a Fajr angle of 18 and an Isha angle of 17.5
//         */
//        KUWAIT,

//        /**
//         * Qatar
//         * Modified version of Umm al-Qura that uses a Fajr angle of 18.
//         */
//        QATAR,
//        SARAJEVO,
//        TEHERAN,
//        JAFARI,

//        /**
//         * The default value for {@link CalculationParameters#method} when initializing a
//         * {@link CalculationParameters} object. Sets a Fajr angle of 0 and an Isha angle of 0.
//         */
//        OTHER

//    }
//    public class PrayerTimeAdjustments
//    {
//        public short Imsak = 0; //default 10 minutes of imsak before Fajr
//        public short Fajr = 0;
//        public short Dhuhr = 0;
//        public short Asr = 0;
//        public short Maghrib = 0;
//        public short Isha = 0;

//        public PrayerTimeAdjustments()
//        {
//            this.Imsak = -10;
//            this.Fajr = 0;
//            this.Dhuhr = 0;
//            this.Asr = 0;
//            this.Maghrib = 0;
//            this.Isha = 0;
//        }
//        public PrayerTimeAdjustments(short imsak, short fajr, short dhuhr, short asr, short maghrib, short isha)
//        {
//            this.Imsak = imsak;
//            this.Fajr = fajr;
//            this.Dhuhr = dhuhr;
//            this.Asr = asr;
//            this.Maghrib = maghrib;
//            this.Isha = isha;
//        }
//    }

//    public class CalculationParameters
//    {
//        public double FajrAngle { get; private set; }
//        public double MaghribAngle { get; private set; }
//        public double IshaAngle { get; private set; }
//        public int IshaIntervalAfterMaghrib { get; private set; }
//        public AsrMethodMadhab AsrMethodCalculation { get; private set; }
//        public HighLatitudeRule HighLatituteRule { get; private set; }
//        public MidnightMethod MidnightMethod { get; private set; }
//        public PrayerTimeAdjustments PrayerTimeAdjustments { get; private set; }
//        public static CalculationMethods SelectedCalculationMethod { get; private set; }

//        public CalculationParameters(double fajrAngle, double ishaAngle)
//        {
//            this.FajrAngle = fajrAngle;
//            this.IshaAngle = ishaAngle;
//            this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
//        }
//        public CalculationParameters(double fajrAngle, double maghribAngle, double ishaAngle)
//        {
//            this.FajrAngle = fajrAngle;
//            this.MaghribAngle = maghribAngle;
//            this.IshaAngle = ishaAngle;
//            this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
//        }
//        public CalculationParameters(double fajrAngle, int ishaIntervalAfterMaghrib)
//        {
//            this.FajrAngle = fajrAngle;
//            this.IshaIntervalAfterMaghrib = ishaIntervalAfterMaghrib;
//            this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
//        }

//        public CalculationParameters(double fajrAngle, double ishaAngle, PrayerTimeAdjustments timeAdjustments)
//            : this(fajrAngle, ishaAngle)
//        {
//            this.PrayerTimeAdjustments = timeAdjustments;
//        }
//        public CalculationParameters(double fajrAngle, int ishaIntervalAfterMaghrib, PrayerTimeAdjustments timeAdjustments)
//            : this(fajrAngle, ishaIntervalAfterMaghrib)
//        {
//            this.PrayerTimeAdjustments = timeAdjustments;
//        }

//        public void SetMidnightMethod(MidnightMethod method)
//        {
//            this.MidnightMethod = method;
//        }
//        public void SetHighLatituteRule(HighLatitudeRule method)
//        {
//            this.HighLatituteRule = method;
//        }
//        public void SetAsrMethodCalculation(AsrMethodMadhab method)
//        {
//            this.AsrMethodCalculation = method;
//        }
//        public static CalculationParameters SetCalculationMethod(CalculationMethods method)
//        {
//            SelectedCalculationMethod = method;

//            switch (method)
//            {
//                case CalculationMethods.MUSLIM_WORLD_LEAGUE:
//                    {
//                        return new CalculationParameters(18.0, 17.0);
//                    }
//                case CalculationMethods.EGYPTIAN:
//                    {
//                        return new CalculationParameters(20.0, 18.0);
//                    }
//                case CalculationMethods.KARACHI:
//                    {
//                        return new CalculationParameters(18.0, 18.0);
//                    }
//                case CalculationMethods.UMM_AL_QURA:
//                    {
//                        return new CalculationParameters(18.5, 90);
//                    }
//                case CalculationMethods.GULF:
//                    {
//                        return new CalculationParameters(19.5, 90);
//                    }
//                case CalculationMethods.MOON_SIGHTING_COMMITTEE:
//                    {
//                        var c= new CalculationParameters(18.0, 18.0, new PrayerTimeAdjustments(-10, 0, 5, 0, 3, 0));
//                        c.SetHighLatituteRule(HighLatitudeRule.OneSeventh);
//                        return c;
//                    }
//                case CalculationMethods.NORTH_AMERICA:
//                    {
//                        return new CalculationParameters(15.0, 15.0);
//                    }
//                case CalculationMethods.KUWAIT:
//                    {
//                        return new CalculationParameters(18.0, 17.5);
//                    }
//                case CalculationMethods.QATAR:
//                    {
//                        return new CalculationParameters(18.0, 90);
//                    }
//                case CalculationMethods.SARAJEVO:
//                    {
//                        return new CalculationParameters(19.0, 17.0, new PrayerTimeAdjustments(-10,-3,2,0,3,0));
//                    }
//                case CalculationMethods.JAFARI:
//                    {
//                        return new CalculationParameters(16.0,4.0,14.0);
//                    }
//                case CalculationMethods.TEHERAN:
//                    {
//                        return new CalculationParameters(17.7, 4.5, 14.0);
//                    }

//                case CalculationMethods.OTHER:
//                    {
//                        return new CalculationParameters(0.0, 0.0);
//                    }
//                default:
//                    {
//                        throw new ArgumentException("Invalid CalculationMethod");
//                    }
//            }
//        }
//    }

//    public class NewTimesCalc
//    {
//        const string CCW = "ccw";
//        timeFormats timeFormat = timeFormats.h_24;
//        private string invalidTime = "-----";
//        //	timeFormats = [
//        //		'24h',         // 24-hour format
//        //		'12h',         // 12-hour format
//        //		'12hNS',       // 12-hour format with no suffix
//        //		'Float'        // floating point number 
//        //	],
//        private enum timeFormats
//        {
//            h_24 = 0,
//            h_12h = 1,
//            h_12hNS = 2,
//            h_Float = 3
//        }

//        public enum ASRMETHOD
//        {
//            Shafii = 1,
//            Hanafi = 2
//        }

//        private enum HIGHLATMETHOD
//        {
//            None = 0,
//            NightMiddle = 1,
//            AngleBased = 2,
//            OneSeventh = 3


//        }

//        private enum MIDHIGHTMETHOD
//        {
//            Standard = 0, // Mid Sunset to Sunrise
//            Jafari = 1 // Mid Sunset to Fajr
//        }

//        class DefaultSetting
//        {
//            public short imsakMinutes;
//            public double fajrAngle;
//            public double dhuhr;
//            public double maghribAngle;
//            public double ishaAngle;
//            public ASRMETHOD asr;
//            public HIGHLATMETHOD highLats;
//            public MIDHIGHTMETHOD midnight;

//            public short maghribMinutes;
//            public short ishaMinutes;

//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="ims">Set number of minutes when imsak begins before sunrise</param>
//            /// <param name="fajrAng">angle to calculate fajr with</param>
//            /// <param name="dhu">dhur begins minutes after suns peek</param>
//            /// <param name="mag"></param>
//            /// <param name="ishaAng">angle with which to calculate isha</param>
//            /// <param name="asrMethod">Juridisc method for ASR</param>
//            /// <param name="highlatmethod"></param>
//            /// <param name="midnightMethod"></param>
//            /// <param name="magribMin">Minutes after sunset, if you put zero then it is as sunset</param>
//            /// <param name="ishaMin">Number of minutes after maghrib isha begins</param>
//            //public DefaultSetting(int ims, double fajrAng, int dhu,double mag, double ishaAng, ASRMETHOD asrMethod,HIGHLATMETHOD highlatmethod,MIDHIGHTMETHOD midnightMethod, short magribMin, short ishaMin)
//            public DefaultSetting(short ims, double fajrAng, int dhu, double maghribAng, double ishaAng,
//                ASRMETHOD asrMethod, HIGHLATMETHOD highlatmethod, MIDHIGHTMETHOD midnightMethod, short magribMin,
//                short ishaMin)
//            {
//                imsakMinutes = ims;
//                fajrAngle = fajrAng;
//                dhuhr = dhu;
//                asr = asrMethod;
//                maghribAngle = maghribAng;
//                ishaAngle = ishaAng;
//                highLats = highlatmethod;
//                midnight = midnightMethod;
//                maghribMinutes = magribMin;
//                ishaMinutes = ishaMin;
//            }

//        }




//        struct Times
//        {
//            public double Imsak;
//            public double Fajr;
//            public double Sunrise;
//            public double Dhuhr;
//            public double Asr;
//            public double Sunset;
//            public double Maghrib;
//            public double Isha;
//            public double Midnight;

//            public Times(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
//                double defaultAsr, double defaultSunset, double defaultMaghrib, double defaultIsha,
//                double defaultMidnight)
//            {
//                Imsak = defaultImsak;
//                Fajr = defaultFajr;
//                Sunrise = defaultSunrise;
//                Dhuhr = defaultDhur;
//                Asr = defaultAsr;
//                Sunset = defaultSunset;
//                Maghrib = defaultMaghrib;
//                Isha = defaultIsha;
//                Midnight = defaultMidnight;

//            }

//        }

//        //DefaultSetting _setting;

//        //private DateTime DateTimeFormat { get; set; }


//        private int numIterations = 1;
//        //private int selectedAsrMethod;
//        private int[] offset;
//        //private bool _useMoonSight = false;

//        public NewTimesCalc(bool useMonsight)
//        {
//            offset = new int[9] {-10, 0, 0, 0, 0, 0, 0, 0, 0};
//            //_useMoonSight = useMonsight;
//            //this.selectedAsrMethod = (int) ASRMETHOD.Shafii;
//            //_setting = new DefaultSetting(10, 18, 0, 0, 17, ASRMETHOD.Shafii, HIGHLATMETHOD.OneSeventh,
//            //    MIDHIGHTMETHOD.Standard, 0, 0);
//            //this.CalculationParameters= CalculationParameters.SetCalculationMethod(CalculationMethods.MOON_SIGHTING_COMMITTEE);
//            //this.CalculationParameters= new CalculationParameters(18.0,17.0, new PrayerTimeAdjustments(10,0,0,0,0,0));
//            //setting = new DefaultSetting(10, 18, 0, 0, 17, ASRMETHOD.Shafii, HIGHLATMETHOD.NightMiddle, MIDHIGHTMETHOD.Standard, 0, 0);
//            //_setting = new DefaultSetting(10, 18, 5, 0, 17, ASRMETHOD.Shafii, HIGHLATMETHOD.OneSeventh, MIDHIGHTMETHOD.Standard, 3, 0);
//        }

//        #region Properties
//        private double JDate { get; set; }
//        private DateComponent DateComponent;
//        private Coordinates Coordinates;
//        private CalculationParameters CalculationParameters;
//        #endregion
//        public string[] getTimes(DateComponent dateComponent, Coordinates coordinates, CalculationMethods method)
//        {
//            this.DateComponent = dateComponent;
//            this.Coordinates = coordinates;
//            this.CalculationParameters = CalculationParameters.SetCalculationMethod(method);
//            //if(coordinates.Latitude>49)
//            //    CalculationParameters.SetHighLatituteRule(HighLatitudeRule.OneSeventh);

//            this.JDate = this.julian(dateComponent.CalculationDate.Year, dateComponent.CalculationDate.Month, dateComponent.CalculationDate.Day) - coordinates.Longtitud / (15 * 24);

//            return this.computeTimes1();
//        }
//        public string[] getTimes(DateComponent dateComponent, Coordinates coordinates, CalculationParameters calculationParameters)
//        {
//            this.DateComponent = dateComponent;
//            this.Coordinates = coordinates;
//            this.JDate = this.julian(dateComponent.CalculationDate.Year, dateComponent.CalculationDate.Month, dateComponent.CalculationDate.Day) - coordinates.Longtitud / (15 * 24);

//            //this.CalculationParameters = calculationParameters;

//            //return this.computeTimes();
//            return this.computeTimes1();
//        }
//        private string[] computeTimes1()
//        {
//            Times times = new Times(5, 5, 6, 12, 13, 18, 18, 18, 18);

//            for (int i = 0; i < this.numIterations; i++)
//            {
//                times = this.computePrayerTimes(times);
//            }

//            times = this.adjustTimes(times);

//            times.Midnight = (CalculationParameters.MidnightMethod == MidnightMethod.Jafari)
//                ? times.Sunset + this.timeDiff(times.Sunset, times.Fajr)/2
//                : times.Sunset + this.timeDiff(times.Sunset, times.Sunrise)/2;

//            times = this.tuneTimes(times);
//            return this.adjustTimesFormat(times);
//        }
//        private Times computePrayerTimes(Times times)
//        {
//            times = this.dayPortion(times);

//            var sunrise = this.sunAngleTime(this.riseSetAngle(), times.Sunrise, CCW);
//            var sunset = this.sunAngleTime(this.riseSetAngle(), times.Sunset);
//            var fajr = CalculationParameters.SelectedCalculationMethod==CalculationMethods.MOON_SIGHTING_COMMITTEE ? MoonSightFarj(sunrise) : this.sunAngleTime(CalculationParameters.FajrAngle, times.Fajr, CCW);
//            var dhuhr = this.midDay(times.Dhuhr);
//            var asr = this.asrTime((int)(CalculationParameters.AsrMethodCalculation+1), times.Asr);
//            var maghrib = this.sunAngleTime(CalculationParameters.MaghribAngle, times.Maghrib);
//            var isha = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? MoonSightIsha(sunset) : this.sunAngleTime(CalculationParameters.IshaAngle, times.Isha);

//            //return new Times(times.Imsak, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha, 0.0);
//            return new Times(fajr, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha, 0.0);
//        }


//        private string[] adjustTimesFormat(Times times)
//        {
//            string[] formatted = new String[9];

//            if (this.timeFormat == timeFormats.h_Float)
//            {
//                formatted[0] = times.Imsak.ToString(CultureInfo.InvariantCulture);
//                formatted[1] = times.Fajr.ToString(CultureInfo.InvariantCulture);
//                formatted[2] = times.Sunrise.ToString(CultureInfo.InvariantCulture);
//                formatted[3] = times.Dhuhr.ToString(CultureInfo.InvariantCulture);
//                formatted[4] = times.Asr.ToString(CultureInfo.InvariantCulture);
//                formatted[5] = times.Sunset.ToString(CultureInfo.InvariantCulture);
//                formatted[6] = times.Maghrib.ToString(CultureInfo.InvariantCulture);
//                formatted[7] = times.Isha.ToString(CultureInfo.InvariantCulture);
//                formatted[8] = times.Midnight.ToString(CultureInfo.InvariantCulture);

//                return formatted;
//            }
//            if (this.timeFormat == timeFormats.h_12h)
//            {
//                formatted[0] = this.floatToTime12(times.Imsak, true);
//                formatted[1] = this.floatToTime12(times.Fajr, true);
//                formatted[2] = this.floatToTime12(times.Sunrise, true);
//                formatted[3] = this.floatToTime12(times.Dhuhr, true);
//                formatted[4] = this.floatToTime12(times.Asr, true);
//                formatted[5] = this.floatToTime12(times.Sunset, true);
//                formatted[6] = this.floatToTime12(times.Maghrib, true);
//                formatted[7] = this.floatToTime12(times.Isha, true);
//                formatted[8] = this.floatToTime12(times.Midnight, true);

//            }
//            else if (this.timeFormat == timeFormats.h_12hNS)
//            {
//                formatted[0] = this.floatToTime12NS(times.Imsak);
//                formatted[1] = this.floatToTime12NS(times.Fajr);
//                formatted[2] = this.floatToTime12NS(times.Sunrise);
//                formatted[3] = this.floatToTime12NS(times.Dhuhr);
//                formatted[4] = this.floatToTime12NS(times.Asr);
//                formatted[5] = this.floatToTime12NS(times.Sunset);
//                formatted[6] = this.floatToTime12NS(times.Maghrib);
//                formatted[7] = this.floatToTime12NS(times.Isha);
//                formatted[8] = this.floatToTime12NS(times.Midnight);

//            }
//            else
//            {
//                formatted[0] = this.floatToTime24(times.Imsak);
//                formatted[1] = this.floatToTime24(times.Fajr);
//                formatted[2] = this.floatToTime24(times.Sunrise);
//                formatted[3] = this.floatToTime24(times.Dhuhr);
//                formatted[4] = this.floatToTime24(times.Asr);
//                formatted[5] = this.floatToTime24(times.Sunset);
//                formatted[6] = this.floatToTime24(times.Maghrib);
//                formatted[7] = this.floatToTime24(times.Isha);
//                formatted[8] = this.floatToTime24(times.Midnight);
//            }
//            return formatted;
//        }


//        private Times tuneTimes(Times times)
//        {

//            times.Fajr += CalculationParameters.PrayerTimeAdjustments.Fajr/60.0;
//            times.Imsak = times.Fajr + CalculationParameters.PrayerTimeAdjustments.Imsak / 60.0;
//            times.Dhuhr += CalculationParameters.PrayerTimeAdjustments.Dhuhr/60.0;
//            times.Asr += CalculationParameters.PrayerTimeAdjustments.Asr/60.0;
//            times.Maghrib += CalculationParameters.PrayerTimeAdjustments.Maghrib/60.0;
//            times.Isha += CalculationParameters.PrayerTimeAdjustments.Isha/60.0;

//            return times;
//        }

//        private Times adjustTimes(Times times)
//        {
//            //var parameters = _setting;

//            times.Imsak += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Fajr += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Sunrise += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Dhuhr += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Asr += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Sunset += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Maghrib += DateComponent.TimeZone - Coordinates.Longtitud / 15;
//            times.Isha += DateComponent.TimeZone - Coordinates.Longtitud / 15;

//            if (CalculationParameters.HighLatituteRule != HighLatitudeRule.None)
//                times = this.adjustHighLats(times);

//            //if (CalculationParameters.PrayerTimeAdjustments.Maghrib >= 0 && Math.Abs(CalculationParameters.MaghribAngle) < 0.001)
//            if (Math.Abs(CalculationParameters.MaghribAngle) < 0.001)
//                times.Maghrib = times.Sunset + CalculationParameters.MaghribAngle/60.0;
//            if (CalculationParameters.IshaIntervalAfterMaghrib > 0)
//                //times.Isha = times.Maghrib + CalculationParameters.IshaAngle/60.0;
//                times.Isha = times.Maghrib + CalculationParameters.IshaIntervalAfterMaghrib / 60.0;

//            //times.Dhuhr += CalculationParameters.PrayerTimeAdjustments.Dhuhr/60.0;

//            return times;
//        }


//        private Times adjustHighLats(Times times)
//        {
//            //var parameters = _setting;
//            var nightTime = this.timeDiff(times.Sunset, times.Sunrise);

//            times.Imsak = this.adjustHLTime(times.Imsak, times.Sunrise, CalculationParameters.PrayerTimeAdjustments.Imsak, nightTime, CCW);
//            times.Fajr = this.adjustHLTime(times.Fajr, times.Sunrise, CalculationParameters.FajrAngle, nightTime, CCW);
//            times.Isha = this.adjustHLTime(times.Isha, times.Sunset, CalculationParameters.IshaAngle, nightTime);
//            times.Maghrib = this.adjustHLTime(times.Isha, times.Sunset, CalculationParameters.PrayerTimeAdjustments.Maghrib, nightTime);

//            return times;
//        }

//        //time, base, angle, night, directio
//        private double adjustHLTime(double time, double bases, double angle, double night, string direction = "NONE")
//        {
//            var portion = this.nightPortion(angle, night);
//            var timeDiff = (direction == CCW)
//                ? this.timeDiff(time, bases)
//                : this.timeDiff(bases, time);
//            if (double.IsNaN(time) || timeDiff > portion)
//                time = bases + (direction == CCW ? -portion : portion);
//            return time;
//        }

//        private double nightPortion(double angle, double night)
//        {
//            //var method = _setting.highLats;
//            var portion = 0.0;

//            if (CalculationParameters.HighLatituteRule == HighLatitudeRule.AngleBased)
//                portion = 1.0/60.0*angle;
//            if (CalculationParameters.HighLatituteRule == HighLatitudeRule.OneSeventh)
//                portion = 1.0/7.0;
//            if (CalculationParameters.HighLatituteRule == HighLatitudeRule.NightMiddle)
//                portion = 1.0/2.0;

//            return portion*night;
//        }



//        private double sunAngleTime(double angle, double time, string direction = "NODIR")
//        {
//            var decl = this.sunPosition(JDate + time).declination;
//            var noon = this.midDay(time);
//            var t = (1/15.0)*
//                    this.arccos((-this.dsin(angle) - this.dsin(decl)*this.dsin(Coordinates.Latitude))/(this.dcos(decl)*this.dcos(Coordinates.Latitude)));
//            return noon + (direction == CCW ? -t : t);
//        }

//        private sunAngleT sunPosition(double jd)
//        {
//            double D = jd - 2451545.0;
//            double g = this.FixAngle(357.529 + 0.98560028*D);
//            double q = this.FixAngle(280.459 + 0.98564736*D);
//            double L = this.FixAngle(q + 1.915*this.dsin(g) + 0.020*this.dsin(2*g));

//            double R = 1.00014 - 0.01671*this.dcos(g) - 0.00014*this.dcos(2*g);
//            double e = 23.439 - 0.00000036*D;
//            var RA = this.arctan2(this.dcos(e)*this.dsin(L), this.dcos(L))/15;
//            var eqt = q/15 - this.FixHour(RA);
//            var decl = this.darcsin(this.dsin(e)*this.dsin(L));

//            return new sunAngleT(decl, eqt);

//        }

//        struct sunAngleT

//        {
//            public readonly double declination;
//            public readonly double equation;

//            public sunAngleT(double d, double e)
//            {
//                declination = d;
//                equation = e;
//            }
//        }

//        private double midDay(double time)
//        {
//            var eqt = this.sunPosition(JDate + time).equation;
//            var noon = this.FixHour(12 - eqt);
//            return noon;
//        }

//        private double asrTime(int factor, double time)
//        {
//            var decl = this.sunPosition(JDate+ time).declination;
//            var angle = -this.arccot(factor + this.dtan(Math.Abs(Coordinates.Latitude - decl)));
//            return this.sunAngleTime(angle, time);
//        }

//        private double MoonSightIsha(double sunset)
//        {
//            var minutes = CalculateIshaMinimumGeneral(Coordinates.Latitude, DateComponent.CalculationDate);
//            return sunset + (minutes/60);
//        }

//        private double MoonSightFarj(double sunrise)
//        {
//            var minutes = CalculateFarjMinimumGeneral(Coordinates.Latitude, DateComponent.CalculationDate);
//            return sunrise - (minutes/60);
//        }

//        private double FixAngle(double angle)
//        {
//            return this.fix(angle, 360);
//        }

//        private double FixHour(double a)
//        {
//            return this.fix(a, 24);
//        }

//        private double fix(double a, int b)
//        {
//            a = a - b*(Math.Floor(a/b));
//            return (a < 0) ? a + b : a;
//        }



//        private double[] dayPortion(double[] times)
//        {
//            for (int i = 0; i < times.Length; i++)
//            {
//                times[i] /= 24;
//            }
//            return times;
//        }

//        private Times dayPortion(Times times)
//        {
//            times.Imsak /= 24;
//            times.Imsak /= 24;
//            times.Fajr /= 24;
//            times.Sunrise /= 24;
//            times.Dhuhr /= 24;
//            times.Asr /= 24;
//            times.Sunset /= 24;
//            times.Maghrib /= 24;
//            times.Isha /= 24;

//            return times;
//        }

//        private double julian(int year, int month, int day)
//        {
//            if (month <= 2)
//            {
//                year -= 1;
//                month += 12;
//            }
//            double A = (double) Math.Floor(year/100.0);
//            double B = 2 - A + Math.Floor(A/4);

//            double JD = Math.Floor(365.25*(year + 4716)) + Math.Floor(30.6001*(month + 1)) + day + B - 1524.5;
//            return JD;
//        }

//        private double dsin(double degree)
//        {
//            return Math.Sin(this.DegreeToRadian(degree));
//        }

//        private double dcos(double degree)
//        {
//            return Math.Cos(this.DegreeToRadian(degree));
//        }

//        private double dtan(double degree)
//        {
//            return Math.Tan(this.DegreeToRadian(degree));
//        }

//        private double darcsin(double degree)
//        {
//            return this.RadianToDegree(Math.Asin(degree));
//        }

//        private double arccos(double degree)
//        {
//            return this.RadianToDegree(Math.Acos(degree));
//        }

//        private double arctan(double degree)
//        {
//            return this.RadianToDegree(Math.Atan(degree));

//        }

//        private double arccot(double degree)
//        {
//            return this.RadianToDegree(Math.Atan(1/degree));
//        }

//        private double arctan2(double y, double x)
//        {
//            return this.RadianToDegree(Math.Atan2(y, x));
//        }

//        private double DegreeToRadian(double degree)
//        {
//            return (degree*Math.PI)/180.0;
//        }

//        private double RadianToDegree(double radian)
//        {
//            return (radian*180.0)/Math.PI;
//        }

//        //public void setAsrCalculationMethod(ASRMETHOD methodId)
//        //{
//        //    this.selectedAsrMethod = (int) methodId;
//        //}

//        private double riseSetAngle()
//        {
//            var angle = 0.0347*Math.Sqrt(Coordinates.Elevation); // an approximation
//            return 0.833 + angle;
//        }

//        private double timeDiff(double time1, double time2)
//        {
//            return this.FixHour(time2 - time1);
//        }

//        private String floatToTime24(double time)
//        {
//            if (time < 0)
//                return invalidTime;
//            time = this.FixHour(time + 0.5/60); // add 0.5 minutes to round
//            double hours = Math.Floor(time);
//            double minutes = Math.Floor((time - hours)*60);
//            return this.twoDigitsFormat((int) hours) + ":" + this.twoDigitsFormat((int) minutes);
//        }

//        // convert float hours to 12h format
//        private String floatToTime12(double time, bool noSuffix)
//        {
//            if (time < 0)
//                return invalidTime;
//            time = this.FixHour(time + 0.5/60); // add 0.5 minutes to round
//            double hours = Math.Floor(time);
//            double minutes = Math.Floor((time - hours)*60);
//            String suffix = hours >= 12 ? " pm" : " am";
//            hours = (hours + 12 - 1)%12 + 1;
//            return ((int) hours) + ":" + this.twoDigitsFormat((int) minutes) + (noSuffix ? "" : suffix);
//        }

//        private String twoDigitsFormat(int num)
//        {

//            return (num < 10) ? "0" + num : num + "";
//        }

//        // convert float hours to 12h format with no suffix
//        private String floatToTime12NS(double time)
//        {
//            return this.floatToTime12(time, true);
//        }

//        private static double CalculateFarjMinimumGeneral(double lt, DateTime dateTime)
//        {
//            var A = 75 + (28.65/55)*Math.Abs(lt);
//            var B = 75 + (19.44/55)*Math.Abs(lt);
//            var C = 75 + (32.74/55)*Math.Abs(lt);
//            var D = 75 + (48.1/55)*Math.Abs(lt);
//            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);

//            double minimum = 0;

//            if (DYY < 91)
//                minimum = A + ((B - A)/91)*DYY;
//            else if (DYY < 137)
//                minimum = B + ((C - B)/46)*(DYY - 91);
//            else if (DYY < 183)
//                minimum = C + ((D - C)/46)*(DYY - 137);
//            else if (DYY < 229)
//                minimum = D + ((C - D)/46)*(DYY - 183);
//            else if (DYY < 275)
//                minimum = C + ((B - C)/46)*(DYY - 229);
//            else if (DYY >= 275)
//                minimum = B + ((A - B)/91)*(DYY - 275);

//            return minimum;
//        }

//        private static double CalculateIshaMinimumAhmer(double lt, DateTime dateTime)
//        {
//            var A = 62 + (17.4/55)*Math.Abs(lt);
//            var B = 62 - (7.16/55)*Math.Abs(lt);
//            var C = 62 + (5.12/55)*Math.Abs(lt);
//            var D = 62 + (19.44/55)*Math.Abs(lt);
//            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
//            double minimum = 0;

//            if (DYY < 91)
//                minimum = A + ((B - A)/91)*DYY;
//            else if (DYY < 137)
//                minimum = B + ((C - B)/46)*(DYY - 91);
//            else if (DYY < 183)
//                minimum = C + ((D - C)/46)*(DYY - 137);
//            else if (DYY < 229)
//                minimum = D + ((C - D)/46)*(DYY - 183);
//            else if (DYY < 275)
//                minimum = C + ((B - C)/46)*(DYY - 229);
//            else if (DYY >= 275)
//                minimum = B + ((A - B)/91)*(DYY - 275);

//            return minimum;
//        }

//        private static double CalculateIshaMinimumAbyad(double lt, DateTime dateTime)
//        {
//            var A = 75 + (25.6/55)*Math.Abs(lt);
//            var B = 75 + (7.16/55)*Math.Abs(lt);
//            var C = 75 + (36.84/55)*Math.Abs(lt);
//            var D = 75 + (81.84/55)*Math.Abs(lt);
//            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
//            double minimum = 0;

//            if (DYY < 91)
//                minimum = A + ((B - A)/91)*DYY;
//            else if (DYY < 137)
//                minimum = B + ((C - B)/46)*(DYY - 91);
//            else if (DYY < 183)
//                minimum = C + ((D - C)/46)*(DYY - 137);
//            else if (DYY < 229)
//                minimum = D + ((C - D)/46)*(DYY - 183);
//            else if (DYY < 275)
//                minimum = C + ((B - C)/46)*(DYY - 229);
//            else if (DYY >= 275)
//                minimum = B + ((A - B)/91)*(DYY - 275);

//            return minimum;
//        }

//        private static double CalculateIshaMinimumGeneral(double lt, DateTime dateTime)
//        {
//            double A = 75 + (25.6/55.0)*Math.Abs(lt);
//            double B = 75 + (2.05/55.0)*Math.Abs(lt);
//            double C = 75 - (9.21/55.0)*Math.Abs(lt);
//            double D = 75 + (6.14/55.0)*Math.Abs(lt);
//            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
//            double minimum = 0;

//            if (DYY < 91)
//                minimum = A + ((B - A)/91)*DYY;
//            else if (DYY < 137)
//                minimum = B + ((C - B)/46)*(DYY - 91);
//            else if (DYY < 183)
//                minimum = C + ((D - C)/46)*(DYY - 137);
//            else if (DYY < 229)
//                minimum = D + ((C - D)/46)*(DYY - 183);
//            else if (DYY < 275)
//                minimum = C + ((B - C)/46)*(DYY - 229);
//            else if (DYY >= 275)
//                minimum = B + ((A - B)/91)*(DYY - 275);

//            return minimum;
//        }

//        static int daysSinceSolstice(int dayOfYear, int year, double latitude)
//        {
//            int daysSinceSolistice;
//            int northernOffset = 10;
//            bool isLeapYear = DateTime.IsLeapYear(year);
//            int southernOffset = isLeapYear ? 173 : 172;
//            int daysInYear = isLeapYear ? 366 : 365;

//            if (latitude >= 0)
//            {
//                daysSinceSolistice = dayOfYear + northernOffset;
//                if (daysSinceSolistice >= daysInYear)
//                {
//                    daysSinceSolistice = daysSinceSolistice - daysInYear;
//                }
//            }
//            else
//            {
//                daysSinceSolistice = dayOfYear - southernOffset;
//                if (daysSinceSolistice < 0)
//                {
//                    daysSinceSolistice = daysSinceSolistice + daysInYear;
//                }
//            }
//            return daysSinceSolistice;
//        }
//    }
//}

using System;
using System.Globalization;

namespace SalahTimes.Experiments
{



    //public enum AsrMethodMadhab
    //{
    //    Shafii = 1, //Shadow lenght
    //    Hanafi = 2 //Double shadow length
    //}
    //public enum HighLatitudeRule
    //{
    //    None,
    //    NightMiddle,
    //    AngleBased,
    //    OneSeventh
    //}

    //public enum MidnightMethod
    //{
    //    Standard, // Mid Sunset to Sunrise
    //    Jafari // Mid Sunset to Fajr--Shia
    //}

    //public enum TimeFormats
    //{
    //    Hour24,
    //    Hour12,
    //    Hour12hNS,
    //    Hfloat
    //}
    public enum CalculationMethods
    {
        /**
         * Muslim World League
         * Uses Fajr angle of 18 and an Isha angle of 17
         */
        MUSLIM_WORLD_LEAGUE,

        /**
         * Egyptian General Authority of Survey
         * Uses Fajr angle of 19.5 and an Isha angle of 17.5
         */
        EGYPTIAN,

        /**
         * University of Islamic Sciences, Karachi
         * Uses Fajr angle of 18 and an Isha angle of 18
         */
        KARACHI,

        /**
         * Umm al-Qura University, Makkah
         * Uses a Fajr angle of 18.5 and an Isha angle of 90. Note: You should add a +30 minute custom
         * adjustment of Isha during Ramadan.
         */
        UMM_AL_QURA,

        /**
         * The Gulf Region
         * Modified version of Umm al-Qura that uses a Fajr angle of 19.5.
         */
        GULF,

        /**
         * Moonsighting Committee
         * Uses a Fajr angle of 18 and an Isha angle of 18. Also uses seasonal adjustment values.
         */
        MOON_SIGHTING_COMMITTEE,

        /**
         * Referred to as the ISNA method
         * This method is included for completeness, but is not recommended.
         * Uses a Fajr angle of 15 and an Isha angle of 15.
         */
        NORTH_AMERICA,

        /**
         * Kuwait
         * Uses a Fajr angle of 18 and an Isha angle of 17.5
         */
        KUWAIT,

        /**
         * Qatar
         * Modified version of Umm al-Qura that uses a Fajr angle of 18.
         */
        QATAR,

        /**
         * The default value for {@link CalculationParameters#method} when initializing a
         * {@link CalculationParameters} object. Sets a Fajr angle of 0 and an Isha angle of 0.
         */
        OTHER

    }
    //public class PrayerTimeAdjustments
    //{
    //    public short Imsak = 0; //default 10 minutes of imsak before Fajr
    //    public short Fajr = 0;
    //    public short Dhuhr = 0;
    //    public short Asr = 0;
    //    public short Maghrib = 0;
    //    public short Isha = 0;

    //    public PrayerTimeAdjustments()
    //    {
    //        this.Imsak = 10;
    //        this.Fajr = 0;
    //        this.Dhuhr = 0;
    //        this.Asr = 0;
    //        this.Maghrib = 0;
    //        this.Isha = 0;
    //    }
    //    public PrayerTimeAdjustments(short imsak, short fajr, short dhuhr, short asr, short maghrib, short isha)
    //    {
    //        this.Imsak = imsak;
    //        this.Fajr = fajr;
    //        this.Dhuhr = dhuhr;
    //        this.Asr = asr;
    //        this.Maghrib = maghrib;
    //        this.Isha = isha;
    //    }
    //}

    //public class PrayerTimes
    //{
    //    public double Imsak;
    //    public double Fajr;
    //    public double Sunrise;
    //    public double Dhuhr;
    //    public double Asr;
    //    public double Sunset;
    //    public double Maghrib;
    //    public double Isha;
    //    public double Midnight;

    //    public PrayerTimes(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
    //        double defaultAsr, double defaultSunset, double defaultMaghrib, double defaultIsha,
    //        double defaultMidnight)
    //    {
    //        Imsak = defaultImsak;
    //        Fajr = defaultFajr;
    //        Sunrise = defaultSunrise;
    //        Dhuhr = defaultDhur;
    //        Asr = defaultAsr;
    //        Sunset = defaultSunset;
    //        Maghrib = defaultMaghrib;
    //        Isha = defaultIsha;
    //        Midnight = defaultMidnight;
    //    }

    //    //private PrayerTimes _dayPortion;
    //    //private PrayerTimes DayPortion => _dayPortion ?? 
    //    //                                    (_dayPortion = new PrayerTimes(this.Imsak/24,
    //    //                                                        this.Fajr/24,
    //    //                                                        this.Sunrise/24,
    //    //                                                        this.Dhuhr/24,
    //    //                                                        this.Asr/24,
    //    //                                                        this.Sunset/24,
    //    //                                                        this.Maghrib/24,
    //    //                                                        this.Isha/24,
    //    //                                                        this.Midnight));
    //}
    //public class Coordinates
    //{
    //    public double Latitude { get; private set; }
    //    public double Longtitud { get; private set; }

    //    public double Elevation { get; private set; }

    //    public Coordinates(double lat, double lon)
    //    {
    //        Latitude = lat;
    //        Longtitud = lon;
    //        Elevation = 0;
    //    }
    //    public Coordinates(double lat, double lon, int elevation) : this(lat, lon)
    //    {
    //        Elevation = elevation;
    //    }
    //}

    //public class CalculationParameters
    //{
    //    public double FajrAngle { get; private set; }
    //    public double MaghribAngle { get; private set; }
    //    public double IshaAngle { get; private set; }
    //    public int IshaIntervalAfterMaghrib { get; private set; }
    //    public AsrMethodMadhab AsrMethodCalculation { get; private set; }
    //    public HighLatitudeRule HighLatituteRule { get; private set; }
    //    public MidnightMethod MidnightMethod { get; private set; }
    //    public PrayerTimeAdjustments PrayerTimeAdjustments { get; private set; }

    //    public CalculationParameters(double fajrAngle, double ishaAngle)
    //    {
    //        this.FajrAngle = fajrAngle;
    //        this.IshaAngle = ishaAngle;
    //        this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
    //    }

    //    public CalculationParameters(double fajrAngle, int ishaIntervalAfterMaghrib)
    //    {
    //        this.FajrAngle = fajrAngle;
    //        this.IshaIntervalAfterMaghrib = ishaIntervalAfterMaghrib;
    //        this.PrayerTimeAdjustments = new PrayerTimeAdjustments();
    //    }

    //    public CalculationParameters(double fajrAngle, double ishaAngle, PrayerTimeAdjustments timeAdjustments)
    //        : this(fajrAngle, ishaAngle)
    //    {
    //        this.PrayerTimeAdjustments = timeAdjustments;
    //    }

    //    public CalculationParameters GetParameters(CalculationMethods method)
    //    {
    //        switch (method)
    //        {
    //            case CalculationMethods.MUSLIM_WORLD_LEAGUE:
    //                {
    //                    return new CalculationParameters(18.0, 17.0);
    //                }
    //            case CalculationMethods.EGYPTIAN:
    //                {
    //                    return new CalculationParameters(20.0, 18.0);
    //                }
    //            case CalculationMethods.KARACHI:
    //                {
    //                    return new CalculationParameters(18.0, 18.0);
    //                }
    //            case CalculationMethods.UMM_AL_QURA:
    //                {
    //                    return new CalculationParameters(18.5, 90);
    //                }
    //            case CalculationMethods.GULF:
    //                {
    //                    return new CalculationParameters(19.5, 90);
    //                }
    //            case CalculationMethods.MOON_SIGHTING_COMMITTEE:
    //                {
    //                    return new CalculationParameters(18.0, 18.0);
    //                }
    //            case CalculationMethods.NORTH_AMERICA:
    //                {
    //                    return new CalculationParameters(15.0, 15.0);
    //                }
    //            case CalculationMethods.KUWAIT:
    //                {
    //                    return new CalculationParameters(18.0, 17.5);
    //                }
    //            case CalculationMethods.QATAR:
    //                {
    //                    return new CalculationParameters(18.0, 90);
    //                }
    //            case CalculationMethods.OTHER:
    //                {
    //                    return new CalculationParameters(0.0, 0.0);
    //                }
    //            default:
    //                {
    //                    throw new ArgumentException("Invalid CalculationMethod");
    //                }
    //        }
    //    }
    //}
    //public class DateComponent
    //{
    //    public int TimeZone { get; private set; }
    //    public DateTime CalculationDate { get; private set; }
    //    //JUlianDate
    //    public double JulianDate { get; private set; }

    //    public DateComponent(DateTime calculationDate, int timeZone)
    //    {
    //        this.CalculationDate = calculationDate;
    //        this.TimeZone = timeZone;
    //        this.JulianDate = ToJulianDate(calculationDate.Year, calculationDate.Month, calculationDate.Day);
    //    }
    //    private double ToJulianDate(int year, int month, int day)
    //    {
    //        if (month <= 2)
    //        {
    //            year -= 1;
    //            month += 12;
    //        }
    //        var A = (double)Math.Floor(year / 100.0);
    //        var B = 2 - A + Math.Floor(A / 4);

    //        var JD = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
    //        return JD;
    //    }

    //}

    //public class MathUtilities
    //{
    //    public static double SinRadian(double degree)
    //    {
    //        return Math.Sin(DegreeToRadian(degree));
    //    }

    //    public static double CosRadian(double degree)
    //    {
    //        return Math.Cos(DegreeToRadian(degree));
    //    }
    //    public static double TanRadian(double degree)
    //    {
    //        return Math.Tan(DegreeToRadian(degree));
    //    }

    //    public static double ArcSin(double degree)
    //    {
    //        return RadianToDegree(Math.Asin(degree));
    //    }

    //    public static double ArcCos(double degree)
    //    {
    //        return RadianToDegree(Math.Acos(degree));
    //    }

    //    public static double ArcTan(double degree)
    //    {
    //        return RadianToDegree(Math.Atan(degree));

    //    }

    //    public static double ArcCot(double degree)
    //    {
    //        return RadianToDegree(Math.Atan(1 / degree));
    //    }

    //    public static double ArcTan2(double y, double x)
    //    {
    //        return RadianToDegree(Math.Atan2(y, x));
    //    }

    //    private static double DegreeToRadian(double degree)
    //    {
    //        return (degree * Math.PI) / 180.0;
    //    }

    //    private static double RadianToDegree(double radian)
    //    {
    //        return (radian * 180.0) / Math.PI;
    //    }
    //}
    //public class SunTimes
    //{
    //    public static double SunAngleTime(double angle, double time, double lat, string direction = "NODIR")
    //    {
    //        var decl = sunPosition(JDate + time).declination;
    //        var noon = MidDay(time);
    //        var t = (1 / 15.0) *
    //                MathUtilities.ArcCos((-MathUtilities.SinRadian(angle) - MathUtilities.SinRadian(decl) * MathUtilities.SinRadian(lat)) /
    //                (MathUtilities.CosRadian(decl) * MathUtilities.CosRadian(lat)));
    //        return noon + (direction == CCW ? -t : t);
    //    }

    //    private static sunAngle sunPosition(double jd)
    //    {
    //        double D = jd - 2451545.0;
    //        double g = FixAngle(357.529 + 0.98560028 * D);
    //        double q = FixAngle(280.459 + 0.98564736 * D);
    //        double L = FixAngle(q + 1.915 * MathUtilities.ArcSin(g) + 0.020 * MathUtilities.ArcSin(2 * g));

    //        double R = 1.00014 - 0.01671 * MathUtilities.CosRadian(g) - 0.00014 * MathUtilities.CosRadian(2 * g);
    //        double e = 23.439 - 0.00000036 * D;
    //        var RA = MathUtilities.ArcTan2(MathUtilities.CosRadian(e) * MathUtilities.SinRadian(L), MathUtilities.CosRadian(L)) / 15;
    //        var eqt = q / 15 - FixHour(RA);
    //        var decl = MathUtilities.ArcSin(MathUtilities.SinRadian(e) * MathUtilities.SinRadian(L));

    //        return new sunAngle(decl, eqt);

    //    }

    //    struct sunAngle

    //    {
    //        public readonly double declination;
    //        public readonly double equation;

    //        public sunAngle(double d, double e)
    //        {
    //            declination = d;
    //            equation = e;
    //        }
    //    }
    //    public static double TimeDiff(double time1, double time2)
    //    {
    //        return FixHour(time2 - time1);
    //    }

    //    public static double AsrTime(int factor, double time,double lat)
    //    {
    //        var decl = sunPosition(JDate + time).declination;
    //        var angle = -MathUtilities.ArcCot(factor + MathUtilities.TanRadian(Math.Abs(lat - decl)));
    //        return SunAngleTime(angle, time,lat);
    //    }

    //    public static double MidDay(double time)
    //    {
    //        var eqt = sunPosition(JDate + time).equation;
    //        var noon = FixHour(12 - eqt);
    //        return noon;
    //    }
    //    private static double FixAngle(double angle)
    //    {
    //        return Fix(angle, 360);
    //    }

    //    private static double FixHour(double a)
    //    {
    //        return Fix(a, 24);
    //    }
    //    private static double Fix(double a, int b)
    //    {
    //        a = a - b * (Math.Floor(a / b));
    //        return (a < 0) ? a + b : a;
    //    }
    //    public static double RiseSetAngle(double elevation)
    //    {
    //        var angle = 0.0347 * Math.Sqrt(elevation); // an approximation
    //        return 0.833 + angle;
    //    }

    //}

    //public class PrayerCalc2
    //{

    //    #region"Properties"

    //    private Coordinates Coordinate { get; set; }
    //    private DateComponent DateComponent { get; set; }
    //    private CalculationParameters Parameters { get; set; }

    //    private short _numberOfIterations = 1;

    //    private PrayerTimes _prayerTimes = null;
    //    #endregion
    //    public string[] CalculateTimes(Coordinates coordinates, DateComponent dateComponent, CalculationParameters parameters)
    //    {
    //        this.Coordinate = coordinates;
    //        this.DateComponent = dateComponent;
    //        this.Parameters = parameters;

    //        return this.ProcessPrayerTimes();
    //    }
    //    private string[] ProcessPrayerTimes()
    //    {
    //        _prayerTimes = new PrayerTimes(5, 5, 6, 12, 13, 18, 18, 18, 18);//default values

    //        for (int i = 0; i < this._numberOfIterations; i++)
    //        {
    //            _prayerTimes = this.ComputePrayerTimes(_prayerTimes);
    //        }

    //        _prayerTimes = this.adjustTimes(_prayerTimes);

    //        _prayerTimes.Midnight = (Parameters.MidnightMethod == MidnightMethod.Jafari)
    //            ? _prayerTimes.Sunset + SunTimes.TimeDiff(_prayerTimes.Sunset, _prayerTimes.Fajr) / 2
    //            : _prayerTimes.Sunset + SunTimes.TimeDiff(_prayerTimes.Sunset, _prayerTimes.Sunrise) / 2;

    //        _prayerTimes = this.tuneTimes(_prayerTimes);
    //        return this.adjustTimesFormat(_prayerTimes);
    //    }
    //    private PrayerTimes ComputePrayerTimes(PrayerTimes times)
    //    {
    //        times = this.DayPortion(times);
    //        var parameters = this.Parameters;

    //        var sunrise = SunTimes.SunAngleTime(SunTimes.RiseSetAngle(Coordinate.Elevation), times.Sunrise, Coordinate.Latitude, CCW);
    //        var sunset = SunTimes.SunAngleTime(SunTimes.RiseSetAngle(Coordinate.Elevation), times.Sunset, Coordinate.Latitude);

    //        var fajr = _useMoonSight ? MoonSightFarj(sunrise) : SunTimes.SunAngleTime(parameters.FajrAngle, times.Fajr, Coordinate.Latitude, CCW);
    //        var dhuhr = SunTimes.MidDay(times.Dhuhr);
    //        var asr = SunTimes.AsrTime((int)parameters.AsrMethodCalculation, times.Asr, Coordinate.Latitude);
    //        var maghrib = SunTimes.SunAngleTime(parameters.MaghribAngle, times.Maghrib, Coordinate.Latitude);
    //        var isha = _useMoonSight ? MoonSightIsha(sunset) : SunTimes.SunAngleTime(parameters.IshaAngle, times.Isha, Coordinate.Latitude);

    //        return new PrayerTimes(times.Imsak, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha, 0.0);
    //    }

    //    private PrayerTimes adjustTimes(PrayerTimes times)
    //    {

    //        times.Imsak += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Fajr += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Sunrise += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Dhuhr += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Asr += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Sunset += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Maghrib += DateComponent.TimeZone - Coordinate.Longtitud / 15;
    //        times.Isha += DateComponent.TimeZone - Coordinate.Longtitud / 15;

    //        if (Parameters.HighLatituteRule != HighLatitudeRule.None)
    //            times = this.adjustHighLats(times);

    //        //if (parameters.imsakMinutes >= 0)
    //            times.Imsak = times.Fajr - Parameters.PrayerTimeAdjustments.Imsak / 60.0;
    //        if (parameters.maghribMinutes >= 0 && Math.Abs(parameters.maghribAngle) < 0.001)
    //            times.Maghrib = times.Sunset + parameters.maghribMinutes / 60.0;
    //        if (parameters.ishaMinutes > 0)
    //            times.Isha = times.Maghrib + parameters.ishaAngle / 60.0;
    //        times.Dhuhr += parameters.dhuhr / 60.0;
    //        return times;


    //    }
    //    private PrayerTimes adjustHighLats(PrayerTimes times)
    //    {
    //        var parameters = Parameters;
    //        var nightTime = SunTimes.TimeDiff(times.Sunset, times.Sunrise);

    //        times.Imsak = this.adjustHLTime(times.Imsak, times.Sunrise, parameters.ImsakMinutes, nightTime, CCW);
    //        times.Fajr = this.adjustHLTime(times.Fajr, times.Sunrise, parameters.fajrAngle, nightTime, CCW);
    //        times.Isha = this.adjustHLTime(times.Isha, times.Sunset, parameters.ishaAngle, nightTime);
    //        times.Maghrib = this.adjustHLTime(times.Isha, times.Sunset, parameters.maghribMinutes, nightTime);

    //        return times;
    //    }
    //    private double adjustHLTime(double time, double bases, double angle, double night, string direction = "NONE")
    //    {
    //        var portion = this.nightPortion(angle, night);
    //        var timeDiff = (direction == CCW)
    //            ? this.timeDiff(time, bases)
    //            : this.timeDiff(bases, time);
    //        if (double.IsNaN(time) || timeDiff > portion)
    //            time = bases + (direction == CCW ? -portion : portion);
    //        return time;
    //    }
    //    private PrayerTimes DayPortion(PrayerTimes times)
    //    {
    //        times.Imsak /= 24;
    //        times.Imsak /= 24;
    //        times.Fajr /= 24;
    //        times.Sunrise /= 24;
    //        times.Dhuhr /= 24;
    //        times.Asr /= 24;
    //        times.Sunset /= 24;
    //        times.Maghrib /= 24;
    //        times.Isha /= 24;

    //        return times;
    //    }

    //}

}
namespace SalahTimes.Domain
{
    //public class PrayerCalc
    //{
    //    enum AsrMethodMadhab
    //    {
    //        Shafii = 1, //Shadow lenght
    //        Hanafi = 2 //Double shadow length
    //    }

    //    enum HighLatitudeRule
    //    {
    //        None,
    //        NightMiddle,
    //        AngleBased,
    //        OneSeventh
    //    }

    //    enum MidnightMethod
    //    {
    //        Standard, // Mid Sunset to Sunrise
    //        Jafari // Mid Sunset to Fajr--Shia
    //    }

    //    public class PrayerTimeAdjustments
    //    {
    //        private short Imsak = 10; //default 10 minutes of imsak before Fajr
    //        private short Fajr = 0;
    //        private short Dhuhr = 0;
    //        private short Asr = 0;
    //        private short Maghrib = 0;
    //        private short Isha = 0;

    //        public PrayerTimeAdjustments(short imsak, short fajr, short dhuhr, short asr, short maghrib, short isha)
    //        {
    //            this.Imsak = imsak;
    //            this.Fajr = fajr;
    //            this.Dhuhr = dhuhr;
    //            this.Asr = asr;
    //            this.Maghrib = maghrib;
    //            this.Isha = isha;
    //        }
    //    }

    //    public class CalculationParameter
    //    {
    //        double FajrAngle;
    //        double MaghribAngle;
    //        double IshaAngle;
    //        int IshaIntervalAfterMaghrib;
    //        AsrMethodMadhab AsrMethodCalculation;
    //        HighLatitudeRule HighLatituteRule;
    //        MidnightMethod MidnightMethod;
    //        PrayerTimeAdjustments PrayerTimeAdjustments;

    //        public CalculationParameter(double fajrAngle, double ishaAngle)
    //        {
    //            this.FajrAngle = fajrAngle;
    //            this.IshaAngle = ishaAngle;
    //        }

    //        public CalculationParameter(double fajrAngle, int ishaIntervalAfterMaghrib)
    //        {
    //            this.FajrAngle = fajrAngle;
    //            this.IshaIntervalAfterMaghrib = ishaIntervalAfterMaghrib;
    //        }

    //        public CalculationParameter(double fajrAngle, double ishaAngle, PrayerTimeAdjustments timeAdjustments)
    //            : this(fajrAngle, ishaAngle)
    //        {
    //            this.PrayerTimeAdjustments = timeAdjustments;
    //        }
    //    }

    //    public class PrayerTimes
    //    {
    //        double Imsak;
    //        double Fajr;
    //        double Sunrise;
    //        double Dhuhr;
    //        double Asr;
    //        double Sunset;
    //        double Maghrib;
    //        double Isha;
    //        double Midnight;

    //        public PrayerTimes(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
    //            double defaultAsr, double defaultSunset, double defaultMaghrib, double defaultIsha,
    //            double defaultMidnight)
    //        {
    //            Imsak = defaultImsak;
    //            Fajr = defaultFajr;
    //            Sunrise = defaultSunrise;
    //            Dhuhr = defaultDhur;
    //            Asr = defaultAsr;
    //            Sunset = defaultSunset;
    //            Maghrib = defaultMaghrib;
    //            Isha = defaultIsha;
    //            Midnight = defaultMidnight;
    //        }

    //    }

    //    public class Coordinates
    //    {
    //        public double Latitude { get; private set; }
    //        public double Longtitud { get; private set; }

    //        public double Elevation { get; private set; }

    //        public Coordinates(double lat, double lon, int elevation)
    //        {
    //            Latitude = lat;
    //            Longtitud = lon;
    //            Elevation = elevation;
    //        }
    //    }

    //    public class DateSettings
    //    {
    //        private int TimeZone { get; set; }
    //        private DateTime CalculationDate { get; set; }
    //        //JUlianDate
    //        private double JDate { get; set; }

    //        public DateSettings(DateTime calculationDate, int timeZone)
    //        {

    //            this.TimeZone = timeZone;
    //        }

    //    }

    //    #region"Properties"

    //    private Coordinates Coordinate { get; set; }

    //    #endregion


    //    public void CalculateTimes(DateTime date, Coordinates coordinates, int timeZone, int elevation)
    //    {
    //        this.Coordinate = coordinates;
    //        //this.DateTimeFormat = date;
    //        //this.lat = latitude;
    //        //this.lng = longitude;
    //        //this.elv = dst;
    //        //this.timeZone = timeZoneC;
    //        //this.JDate = this.julian(date.Year, date.Month, date.Day) - longitude / (15 * 24);

    //        //return this.computeTimes1();
    //    }
    //}
    public enum AsrMethodMadhab
    {
        Shafii = 0, //Shadow lenght
        Hanafi = 1 //Double shadow length
    }
    public enum HighLatitudeRule
    {
        None,
        NightMiddle,
        AngleBased,
        OneSeventh
    }

    public enum MidnightMethod
    {
        Standard, // Mid Sunset to Sunrise
        Jafari // Mid Sunset to Fajr--Shia
    }

    public enum TimeFormats
    {
        Hour24,
        Hour12,
        Hour12hNS,
        Hfloat
    }
    public class Coordinates
    {
        public double Latitude { get; private set; }
        public double Longtitud { get; private set; }

        public double Elevation { get; private set; }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longtitud = lon;
            Elevation = 0;
        }
        public Coordinates(double lat, double lon, int elevation) : this(lat, lon)
        {
            Elevation = elevation;
        }
    }

    public class DateComponent
    {
        public int TimeZone { get; private set; }
        public DateTime CalculationDate { get; private set; }
        public TimeFormats TimeFormat { get; private set; }
        //JUlianDate
        //public double JulianDate { get; private set; }

        public DateComponent(DateTime calculationDate, int timeZone)
        {
            this.CalculationDate = calculationDate;
            this.TimeZone = timeZone;
            this.TimeFormat=TimeFormats.Hour24;
            //this.JulianDate = ToJulianDate(calculationDate.Year, calculationDate.Month, calculationDate.Day);
        }
        public DateComponent(DateTime calculationDate,int timeZone, TimeFormats timeFormat):this(calculationDate,timeZone)
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
    public enum CalculationMethods
    {
        /**
         * Muslim World League
         * Uses Fajr angle of 18 and an Isha angle of 17
         */
        MUSLIM_WORLD_LEAGUE,

        /**
         * Egyptian General Authority of Survey
         * Uses Fajr angle of 19.5 and an Isha angle of 17.5
         */
        EGYPTIAN,

        /**
         * University of Islamic Sciences, Karachi
         * Uses Fajr angle of 18 and an Isha angle of 18
         */
        KARACHI,

        /**
         * Umm al-Qura University, Makkah
         * Uses a Fajr angle of 18.5 and an Isha angle of 90. Note: You should add a +30 minute custom
         * adjustment of Isha during Ramadan.
         */
        UMM_AL_QURA,

        /**
         * The Gulf Region
         * Modified version of Umm al-Qura that uses a Fajr angle of 19.5.
         */
        GULF,

        /**
         * Moonsighting Committee
         * Uses a Fajr angle of 18 and an Isha angle of 18. Also uses seasonal adjustment values.
         */
        MOON_SIGHTING_COMMITTEE,

        /**
         * Referred to as the ISNA method
         * This method is included for completeness, but is not recommended.
         * Uses a Fajr angle of 15 and an Isha angle of 15.
         */
        NORTH_AMERICA,

        /**
         * Kuwait
         * Uses a Fajr angle of 18 and an Isha angle of 17.5
         */
        KUWAIT,

        /**
         * Qatar
         * Modified version of Umm al-Qura that uses a Fajr angle of 18.
         */
        QATAR,
        SARAJEVO,
        TEHERAN,
        JAFARI,

        /**
         * The default value for {@link CalculationParameters#method} when initializing a
         * {@link CalculationParameters} object. Sets a Fajr angle of 0 and an Isha angle of 0.
         */
        OTHER

    }

    public class PrayerTimes
    {
        public double Imsak { get; set; }
        public double Fajr { get; set; }
        public double Sunrise { get; set; }
        public double Dhuhr { get; set; }
        public double Asr { get; set; }
        public double Sunset { get; set; }
        public double Maghrib { get; set; }
        public double Isha { get; set; }
        public double Midnight { get; set; }

        public PrayerTimes(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
            double defaultAsr, double defaultSunset, double defaultMaghrib, double defaultIsha,
            double defaultMidnight)
        {
            Imsak = defaultImsak;
            Fajr = defaultFajr;
            Sunrise = defaultSunrise;
            Dhuhr = defaultDhur;
            Asr = defaultAsr;
            Sunset = defaultSunset;
            Maghrib = defaultMaghrib;
            Isha = defaultIsha;
            Midnight = defaultMidnight;

        }
        public PrayerTimes DayPortion(PrayerTimes times)
        {
            times.Imsak /= 24;
            times.Imsak /= 24;
            times.Fajr /= 24;
            times.Sunrise /= 24;
            times.Dhuhr /= 24;
            times.Asr /= 24;
            times.Sunset /= 24;
            times.Maghrib /= 24;
            times.Isha /= 24;

            return times;
        }

    }
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

    public class NewTimesCalc
    {
        
        public NewTimesCalc(bool useMonsight)
        {
        }

        #region Properties
        private double JDate { get; }
        private readonly DateComponent _dateComponent;
        private readonly Coordinates _coordinates;
        private readonly CalculationParameters _calculationParameters;
        private AstronomicalCalculations _astronomicalCalculationsInstance;
        
        private int numIterations=1;

        public NewTimesCalc(DateComponent dateComponent, Coordinates coordinates, CalculationMethods calculationMethod)
        {
            this._dateComponent = dateComponent;
            this._coordinates = coordinates;
            this._calculationParameters = CalculationParameters.SetCalculationMethod(calculationMethod);

            this.JDate = TimeUtilities.Julian(dateComponent.CalculationDate.Year, dateComponent.CalculationDate.Month, dateComponent.CalculationDate.Day)
                - coordinates.Longtitud / (15 * 24);
            _astronomicalCalculationsInstance = new AstronomicalCalculations(calculationMethod);

        }
        #endregion
        public string[] getTimes()
        {
            return this.computeTimes1();
        }

        private string[] computeTimes1()
        {
            var times = new PrayerTimes(5, 5, 6, 12, 13, 18, 18, 18, 18);

            for (var i = 0; i < numIterations; i++)
                times = this.ComputePrayerTimes(times);

            AjdustTimeZone(times);

            AdjustTimes(times);

            TuneTimes(times);

            return this.AdjustTimesFormat(times);
        }
        private PrayerTimes ComputePrayerTimes(PrayerTimes times)
        {
            var portionTimes = times.DayPortion(times);

            var sunrise = AstronomicalCalculations.SunAngleTime(AstronomicalCalculations.RiseSetAngle(_coordinates.Elevation), portionTimes.Sunrise, JDate, _coordinates.Latitude, isCcwDirection: true);
            var sunset = AstronomicalCalculations.SunAngleTime(AstronomicalCalculations.RiseSetAngle(_coordinates.Elevation), portionTimes.Sunset, JDate, _coordinates.Latitude);
            var fajr = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? AstronomicalCalculations.MoonSightFarj(sunrise,_coordinates.Latitude,_dateComponent.CalculationDate) : AstronomicalCalculations.SunAngleTime(_calculationParameters.FajrAngle, portionTimes.Fajr, JDate, _coordinates.Latitude, isCcwDirection: true);
            var dhuhr = AstronomicalCalculations.MidDay(portionTimes.Dhuhr, JDate);
            var asr = AstronomicalCalculations.AsrTime((int)(_calculationParameters.AsrMethodCalculation) + 1, portionTimes.Asr, JDate, _coordinates.Latitude);
            var maghrib = AstronomicalCalculations.SunAngleTime(_calculationParameters.MaghribAngle, portionTimes.Maghrib, JDate, _coordinates.Latitude);
            var isha = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? AstronomicalCalculations.MoonSightIsha(sunset, _coordinates.Latitude, _dateComponent.CalculationDate) : AstronomicalCalculations.SunAngleTime(_calculationParameters.IshaAngle, portionTimes.Isha, JDate, _coordinates.Latitude);
            var midnight = (_calculationParameters.MidnightMethod == MidnightMethod.Jafari)
                ? sunset + TimeUtilities.TimeDiff(sunset, fajr)/2
                : sunset + TimeUtilities.TimeDiff(sunset, sunrise)/2;
            //We set default time for Imsak equal to Fajr
            return new PrayerTimes(fajr, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha, midnight);
        }
        private void TuneTimes(PrayerTimes times)
        {

            times.Fajr += _calculationParameters.PrayerTimeAdjustments.Fajr / 60.0;
            times.Imsak = times.Fajr + _calculationParameters.PrayerTimeAdjustments.Imsak / 60.0;
            times.Dhuhr += _calculationParameters.PrayerTimeAdjustments.Dhuhr / 60.0;
            times.Asr += _calculationParameters.PrayerTimeAdjustments.Asr / 60.0;
            times.Maghrib += _calculationParameters.PrayerTimeAdjustments.Maghrib / 60.0;
            times.Isha += _calculationParameters.PrayerTimeAdjustments.Isha / 60.0;

        }
        private void AdjustTimes(PrayerTimes times)
        {
            if (_calculationParameters.HighLatituteRule != HighLatitudeRule.None)
               this.AdjustHighLats(times);

            if (Math.Abs(_calculationParameters.MaghribAngle) < 0.001)
                times.Maghrib = times.Sunset + _calculationParameters.MaghribAngle / 60.0;
            if (_calculationParameters.IshaIntervalAfterMaghrib > 0)
                times.Isha = times.Maghrib + _calculationParameters.IshaIntervalAfterMaghrib / 60.0;
        }

        private void AjdustTimeZone(PrayerTimes times)
        {
            times.Imsak += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Fajr += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Sunrise += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Dhuhr += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Asr += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Sunset += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Maghrib += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            times.Isha += _dateComponent.TimeZone - _coordinates.Longtitud/15;
        }

        private void AdjustHighLats(PrayerTimes times)
        {
            var nightTime = TimeUtilities.TimeDiff(times.Sunset, times.Sunrise);

            times.Fajr = AstronomicalCalculations.AdjustTimeForHighLatitutes(times.Fajr, times.Sunrise, _calculationParameters.FajrAngle, nightTime, _calculationParameters.HighLatituteRule, isCcwDirection: true);
            times.Isha = AstronomicalCalculations.AdjustTimeForHighLatitutes(times.Isha, times.Sunset, _calculationParameters.IshaAngle, nightTime, _calculationParameters.HighLatituteRule);

        }

        private string[] AdjustTimesFormat(PrayerTimes times)
        {
            var formatted = new string[9];

            switch (this._dateComponent.TimeFormat)
            {
                case TimeFormats.Hfloat:
                    formatted[0] = times.Imsak.ToString(CultureInfo.InvariantCulture);
                    formatted[1] = times.Fajr.ToString(CultureInfo.InvariantCulture);
                    formatted[2] = times.Sunrise.ToString(CultureInfo.InvariantCulture);
                    formatted[3] = times.Dhuhr.ToString(CultureInfo.InvariantCulture);
                    formatted[4] = times.Asr.ToString(CultureInfo.InvariantCulture);
                    formatted[5] = times.Sunset.ToString(CultureInfo.InvariantCulture);
                    formatted[6] = times.Maghrib.ToString(CultureInfo.InvariantCulture);
                    formatted[7] = times.Isha.ToString(CultureInfo.InvariantCulture);
                    formatted[8] = times.Midnight.ToString(CultureInfo.InvariantCulture);
                    break;
                case TimeFormats.Hour12:
                    formatted[0] = times.Imsak.FloatToTime12(true);
                    formatted[1] = TimeUtilities.FloatToTime12(times.Fajr, true);
                    formatted[2] = TimeUtilities.FloatToTime12(times.Sunrise, true);
                    formatted[3] = TimeUtilities.FloatToTime12(times.Dhuhr, true);
                    formatted[4] = TimeUtilities.FloatToTime12(times.Asr, true);
                    formatted[5] = TimeUtilities.FloatToTime12(times.Sunset, true);
                    formatted[6] = TimeUtilities.FloatToTime12(times.Maghrib, true);
                    formatted[7] = TimeUtilities.FloatToTime12(times.Isha, true);
                    formatted[8] = TimeUtilities.FloatToTime12(times.Midnight, true);
                    break;
                case TimeFormats.Hour12hNS:
                    formatted[0] = TimeUtilities.FloatToTime12NS(times.Imsak);
                    formatted[1] = TimeUtilities.FloatToTime12NS(times.Fajr);
                    formatted[2] = TimeUtilities.FloatToTime12NS(times.Sunrise);
                    formatted[3] = TimeUtilities.FloatToTime12NS(times.Dhuhr);
                    formatted[4] = TimeUtilities.FloatToTime12NS(times.Asr);
                    formatted[5] = TimeUtilities.FloatToTime12NS(times.Sunset);
                    formatted[6] = TimeUtilities.FloatToTime12NS(times.Maghrib);
                    formatted[7] = TimeUtilities.FloatToTime12NS(times.Isha);
                    formatted[8] = TimeUtilities.FloatToTime12NS(times.Midnight);
                    break;
                case TimeFormats.Hour24:
                    formatted[0] = times.Imsak.FloatToTime24();
                    formatted[1] = TimeUtilities.FloatToTime24(times.Fajr);
                    formatted[2] = TimeUtilities.FloatToTime24(times.Sunrise);
                    formatted[3] = TimeUtilities.FloatToTime24(times.Dhuhr);
                    formatted[4] = TimeUtilities.FloatToTime24(times.Asr);
                    formatted[5] = TimeUtilities.FloatToTime24(times.Sunset);
                    formatted[6] = TimeUtilities.FloatToTime24(times.Maghrib);
                    formatted[7] = TimeUtilities.FloatToTime24(times.Isha);
                    formatted[8] = TimeUtilities.FloatToTime24(times.Midnight);
                    break;
                default:
                    formatted[0] = TimeUtilities.FloatToTime24(times.Imsak);
                    formatted[1] = TimeUtilities.FloatToTime24(times.Fajr);
                    formatted[2] = TimeUtilities.FloatToTime24(times.Sunrise);
                    formatted[3] = TimeUtilities.FloatToTime24(times.Dhuhr);
                    formatted[4] = TimeUtilities.FloatToTime24(times.Asr);
                    formatted[5] = TimeUtilities.FloatToTime24(times.Sunset);
                    formatted[6] = TimeUtilities.FloatToTime24(times.Maghrib);
                    formatted[7] = TimeUtilities.FloatToTime24(times.Isha);
                    formatted[8] = TimeUtilities.FloatToTime24(times.Midnight);
                    break;
            }
            return formatted;
        }
    }

    public class AstronomicalCalculations
    {
        private static MoonsightCalculationMethod _moonsightCalculationMethod;
        public AstronomicalCalculations(CalculationMethods calculationMethod)
        {
            if(calculationMethod==CalculationMethods.MOON_SIGHTING_COMMITTEE)
                _moonsightCalculationMethod=new MoonsightCalculationMethod();
        }

        private struct SunAngle
        {
            public readonly double declination;
            public readonly double equation;

            public SunAngle(double d, double e)
            {
                declination = d;
                equation = e;
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
        public static double AsrTime(int asrFactor, double time, double julianDate, double latitude)
        {
            var decl = SunPosition(julianDate + time).declination;
            var angle = -MathUtilities.ArcCot(asrFactor + MathUtilities.Tan(Math.Abs(latitude - decl)));
            return SunAngleTime(angle, time, julianDate, latitude);
        }
        //public static double SunAngleTime(double angle, double time, double julianDate, double latitude, string direction = "NODIR")
        public static double SunAngleTime(double angle, double time, double julianDate, double latitude, bool isCcwDirection = false)
        {
            var decl = SunPosition(julianDate + time).declination;
            var noon = MidDay(time, julianDate);
            var t = (1 / 15.0) *
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
        public static double MoonSightIsha(double sunset,double latitude,DateTime calculationDate)
        {
            var minutes = _moonsightCalculationMethod.CalculateIshaMinimumGeneral(latitude, calculationDate);
            return sunset + (minutes / 60);
        }

        public static double MoonSightFarj(double sunrise, double latitude, DateTime calculationDate)
        {
            var minutes = _moonsightCalculationMethod.CalculateFarjMinimumGeneral(latitude, calculationDate);
            return sunrise - (minutes / 60);
        }
        public static double MidDay(double time, double julianDate)
        {
            var eqt = SunPosition(julianDate + time).equation;
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

        public static double AdjustTimeForHighLatitutes(double time, double bases, double angle, double night, HighLatitudeRule highLatitudeRule, bool isCcwDirection = false)
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

        public static double RiseSetAngle(double elevation)
        {
            var angle = 0.0347 * Math.Sqrt(elevation); // an approximation
            return 0.833 + angle;
        }
    }
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
            double A = (double)Math.Floor(year / 100.0);
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

        public static string FloatToTime12NS(this double time)
        {
            return FloatToTime12(time, true);
        }

        public static string FloatToTime24(this double time)
        {
            if (time < 0)
                return InvalidTime;
            time = FixHour(time + 0.5/60); // add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours)*60);
            return TimeUtilities.TwoDigitsFormat((int) hours) + ":" + TwoDigitsFormat((int) minutes);
        }

        public static string FloatToTime12(this double time, bool noSuffix)
        {
            if (time < 0)
                return InvalidTime;
            time = FixHour(time + 0.5/60); // add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours)*60);
            string suffix = hours >= 12 ? " pm" : " am";
            hours = (hours + 12 - 1)%12 + 1;
            return ((int) hours) + ":" + TwoDigitsFormat((int) minutes) + (noSuffix ? "" : suffix);
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
            a = a - b*(Math.Floor(a/b));
            return (a < 0) ? a + b : a;
        }
    }

    public class MoonsightCalculationMethod
    {
        //private double Latitude { get; }
        //private DateTime CalculationDate { get; }
        //public MoonsightCalculationMethod(double latitute, DateTime calculationDate)
        //{
        //    Latitude = latitute;
        //    CalculationDate = calculationDate;
        //}

        public double CalculateIshaMinimumGeneral(double lt, DateTime dateTime)
        {
            double A = 75 + (25.6/55.0)*Math.Abs(lt);
            double B = 75 + (2.05/55.0)*Math.Abs(lt);
            double C = 75 - (9.21/55.0)*Math.Abs(lt);
            double D = 75 + (6.14/55.0)*Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A)/91)*DYY;
            else if (DYY < 137)
                minimum = B + ((C - B)/46)*(DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C)/46)*(DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D)/46)*(DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C)/46)*(DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B)/91)*(DYY - 275);

            return minimum;
        }

        public double CalculateIshaMinimumAbyad(double lt, DateTime dateTime)
        {
            var A = 75 + (25.6/55)*Math.Abs(lt);
            var B = 75 + (7.16/55)*Math.Abs(lt);
            var C = 75 + (36.84/55)*Math.Abs(lt);
            var D = 75 + (81.84/55)*Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A)/91)*DYY;
            else if (DYY < 137)
                minimum = B + ((C - B)/46)*(DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C)/46)*(DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D)/46)*(DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C)/46)*(DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B)/91)*(DYY - 275);

            return minimum;
        }

        public double CalculateFarjMinimumGeneral(double lt, DateTime dateTime)
        {
            var A = 75 + (28.65/55)*Math.Abs(lt);
            var B = 75 + (19.44/55)*Math.Abs(lt);
            var C = 75 + (32.74/55)*Math.Abs(lt);
            var D = 75 + (48.1/55)*Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);

            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A)/91)*DYY;
            else if (DYY < 137)
                minimum = B + ((C - B)/46)*(DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C)/46)*(DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D)/46)*(DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C)/46)*(DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B)/91)*(DYY - 275);

            return minimum;
        }

        public double CalculateIshaMinimumAhmer(double lt, DateTime dateTime)
        {
            var A = 62 + (17.4/55)*Math.Abs(lt);
            var B = 62 - (7.16/55)*Math.Abs(lt);
            var C = 62 + (5.12/55)*Math.Abs(lt);
            var D = 62 + (19.44/55)*Math.Abs(lt);
            var DYY = DaysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
            double minimum = 0;

            if (DYY < 91)
                minimum = A + ((B - A)/91)*DYY;
            else if (DYY < 137)
                minimum = B + ((C - B)/46)*(DYY - 91);
            else if (DYY < 183)
                minimum = C + ((D - C)/46)*(DYY - 137);
            else if (DYY < 229)
                minimum = D + ((C - D)/46)*(DYY - 183);
            else if (DYY < 275)
                minimum = C + ((B - C)/46)*(DYY - 229);
            else if (DYY >= 275)
                minimum = B + ((A - B)/91)*(DYY - 275);

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

    public class PrayerTimes1
    {
        public string ImsakHour => _imsak.DoubleToString(this._dateComponent.TimeFormat);
        public string FajrHour => _fajr.DoubleToString(this._dateComponent.TimeFormat);
        public string SunriseHour =>_sunrise.DoubleToString(this._dateComponent.TimeFormat);
        public string DhuhrHour => _dhuhr.DoubleToString(this._dateComponent.TimeFormat);
        public string AsrHour => _asr.DoubleToString(this._dateComponent.TimeFormat);
        public string SunsetHour => _sunset.DoubleToString(this._dateComponent.TimeFormat);
        public string MaghribHour => _maghrib.DoubleToString(this._dateComponent.TimeFormat);
        public string IshaHour => _isha.DoubleToString(this._dateComponent.TimeFormat);
        public string MidnightHour => _midnight.DoubleToString(this._dateComponent.TimeFormat);

        private double _imsak { get; set; }
        private double _fajr { get; set; }
        private double _sunrise { get; set; }
        private double _dhuhr { get; set; }
        private double _asr { get; set; }
        private double _sunset { get; set; }
        private double _maghrib { get; set; }
        private double _isha { get; set; }
        private double _midnight { get; set; }


        private double _jDate { get; }
        private readonly DateComponent _dateComponent;
        private readonly Coordinates _coordinates;
        private readonly CalculationParameters _calculationParameters;
        private AstronomicalCalculations _astronomicalCalculationsInstance;
        private int _numIterations = 1;

        public PrayerTimes1(DateComponent dateComponent, Coordinates coordinates, CalculationMethods calculationMethod)
        {
            SetStartTimesForCalculation();
            this._dateComponent = dateComponent;
            this._coordinates = coordinates;
            this._calculationParameters = CalculationParameters.SetCalculationMethod(calculationMethod);
            this._jDate = TimeUtilities.Julian(dateComponent.CalculationDate.Year, dateComponent.CalculationDate.Month, dateComponent.CalculationDate.Day) - coordinates.Longtitud/(15*24);

            _astronomicalCalculationsInstance = new AstronomicalCalculations(calculationMethod);
        }

        public void GetTimes()
        {
            this.ComputeTimes();
        }

        private void ComputeTimes()
        {
            for (var i = 0; i < _numIterations; i++)
                ComputePrayerTimes();

            AjdustTimeZone();

            AdjustTimes();

            TuneTimes();
        }

        private void ComputePrayerTimes()
        {
            this.SetTimesDayPortion();

            _sunrise = AstronomicalCalculations.SunAngleTime(AstronomicalCalculations.RiseSetAngle(_coordinates.Elevation), this._sunrise, _jDate, _coordinates.Latitude, isCcwDirection: true);
            _sunset = AstronomicalCalculations.SunAngleTime(AstronomicalCalculations.RiseSetAngle(_coordinates.Elevation), this._sunset, _jDate, _coordinates.Latitude);
            _fajr = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? AstronomicalCalculations.MoonSightFarj(_sunrise, _coordinates.Latitude, _dateComponent.CalculationDate) : AstronomicalCalculations.SunAngleTime(_calculationParameters.FajrAngle, _fajr, _jDate, _coordinates.Latitude, isCcwDirection: true);
            _dhuhr = AstronomicalCalculations.MidDay(this._dhuhr, _jDate);
            _asr = AstronomicalCalculations.AsrTime((int) (_calculationParameters.AsrMethodCalculation) + 1, this._asr, _jDate, _coordinates.Latitude);
            _maghrib = AstronomicalCalculations.SunAngleTime(_calculationParameters.MaghribAngle, this._maghrib, _jDate, _coordinates.Latitude);
            _isha = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? AstronomicalCalculations.MoonSightIsha(_sunset, _coordinates.Latitude, _dateComponent.CalculationDate) : AstronomicalCalculations.SunAngleTime(_calculationParameters.IshaAngle, _isha, _jDate, _coordinates.Latitude);
            _midnight = (_calculationParameters.MidnightMethod == MidnightMethod.Jafari) ? _sunset + TimeUtilities.TimeDiff(_sunset, _fajr)/2 : _sunset + TimeUtilities.TimeDiff(_sunset, _sunrise)/2;
            //We set default time for Imsak equal to Fajr
        }

        private void TuneTimes()
        {
            this._fajr += _calculationParameters.PrayerTimeAdjustments.Fajr/60.0;
            this._imsak = this._fajr + _calculationParameters.PrayerTimeAdjustments.Imsak/60.0;
            this._dhuhr += _calculationParameters.PrayerTimeAdjustments.Dhuhr/60.0;
            this._asr += _calculationParameters.PrayerTimeAdjustments.Asr/60.0;
            this._maghrib += _calculationParameters.PrayerTimeAdjustments.Maghrib/60.0;
            this._isha += _calculationParameters.PrayerTimeAdjustments.Isha/60.0;
        }

        private void AdjustTimes()
        {
            if (_calculationParameters.HighLatituteRule != HighLatitudeRule.None)
                this.AdjustHighLats();

            if (Math.Abs(_calculationParameters.MaghribAngle) < 0.001)
                this._maghrib = this._sunset + _calculationParameters.MaghribAngle/60.0;
            if (_calculationParameters.IshaIntervalAfterMaghrib > 0)
                this._isha = this._maghrib + _calculationParameters.IshaIntervalAfterMaghrib/60.0;
        }

        private void AjdustTimeZone()
        {
            this._imsak += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._fajr += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._sunrise += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._dhuhr += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._asr += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._sunset += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._maghrib += _dateComponent.TimeZone - _coordinates.Longtitud/15;
            this._isha += _dateComponent.TimeZone - _coordinates.Longtitud/15;
        }

        private void AdjustHighLats()
        {
            var nightTime = TimeUtilities.TimeDiff(this._sunset, this._sunrise);

            this._fajr = AstronomicalCalculations.AdjustTimeForHighLatitutes(this._fajr, this._sunrise, _calculationParameters.FajrAngle, nightTime, _calculationParameters.HighLatituteRule, isCcwDirection: true);
            this._isha = AstronomicalCalculations.AdjustTimeForHighLatitutes(this._isha, this._sunset, _calculationParameters.IshaAngle, nightTime, _calculationParameters.HighLatituteRule);
        }

        private void SetStartTimesForCalculation()
        {
            this._imsak = 5;
            this._fajr = 5;
            this._sunrise = 6;
            this._dhuhr = 12;
            this._asr = 13;
            this._sunset = 18;
            this._maghrib = 18;
            this._isha = 18;
            this._midnight = 18;
        }
        private void SetTimesDayPortion()
        {
            this._imsak /= 24;
            this._fajr /= 24;
            this._sunrise /= 24;
            this._dhuhr /= 24;
            this._asr /= 24;
            this._sunset /= 24;
            this._maghrib /= 24;
            this._isha /= 24;
        }
    }
}



