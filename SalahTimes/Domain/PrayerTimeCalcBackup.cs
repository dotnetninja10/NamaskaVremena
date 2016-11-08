using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SalahTimes.Domain
{
    public class PrayerTimeCalcBackup
    {
        const string CCW = "ccw";
        timeFormats timeFormat = timeFormats.h_24;
        private string invalidTime = "-----";
        //	timeFormats = [
        //		'24h',         // 24-hour format
        //		'12h',         // 12-hour format
        //		'12hNS',       // 12-hour format with no suffix
        //		'Float'        // floating point number 
        //	],
        private enum timeFormats
        {
            h_24 = 0,
            h_12h = 1,
            h_12hNS = 2,
            h_Float = 3
        }

        public enum ASRMETHOD
        {
            Shafii = 1,
            Hanafi = 2
        }

        private enum HIGHLATMETHOD
        {
            None = 0,
            NightMiddle = 1,
            AngleBased = 2,
            OneSeventh = 3


        }

        private enum MIDHIGHTMETHOD
        {
            Standard = 0, // Mid Sunset to Sunrise
            Jafari = 1 // Mid Sunset to Fajr
        }

        class DefaultSetting
        {
            public short imsakMinutes;
            public double fajrAngle;
            public double dhuhr;
            public double maghribAngle;
            public double ishaAngle;
            public ASRMETHOD asr;
            public HIGHLATMETHOD highLats;
            public MIDHIGHTMETHOD midnight;

            public short maghribMinutes;
            public short ishaMinutes;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ims">Set number of minutes when imsak begins before sunrise</param>
            /// <param name="fajrAng">angle to calculate fajr with</param>
            /// <param name="dhu">dhur begins minutes after suns peek</param>
            /// <param name="mag"></param>
            /// <param name="ishaAng">angle with which to calculate Isha</param>
            /// <param name="asrMethod">Juridisc method for ASR</param>
            /// <param name="highlatmethod"></param>
            /// <param name="midnightMethod"></param>
            /// <param name="magribMin">Minutes after sunset, if you put zero then it is as sunset</param>
            /// <param name="ishaMin">Number of minutes after Maghrib Isha begins</param>
            //public DefaultSetting(int ims, double fajrAng, int dhu,double mag, double ishaAng, ASRMETHOD asrMethod,HIGHLATMETHOD highlatmethod,MIDHIGHTMETHOD midnightMethod, short magribMin, short ishaMin)
            public DefaultSetting(short ims, double fajrAng, int dhu, double maghribAng, double ishaAng,
                ASRMETHOD asrMethod, HIGHLATMETHOD highlatmethod, MIDHIGHTMETHOD midnightMethod, short magribMin,
                short ishaMin)
            {
                imsakMinutes = ims;
                fajrAngle = fajrAng;
                dhuhr = dhu;
                asr = asrMethod;
                maghribAngle = maghribAng;
                ishaAngle = ishaAng;
                highLats = highlatmethod;
                midnight = midnightMethod;
                maghribMinutes = magribMin;
                ishaMinutes = ishaMin;
            }

        }


        struct Times
        {
            public double Imsak;
            public double Fajr;
            public double Sunrise;
            public double Dhuhr;
            public double Asr;
            public double Sunset;
            public double Maghrib;
            public double Isha;
            public double Midnight;

            public Times(double defaultImsak, double defaultFajr, double defaultSunrise, double defaultDhur,
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

        }

        DefaultSetting _setting;

        private DateTime DateTimeFormat { get; set; }

        private double lat;
        private double lng;
        private int elv;
        private double timeZone;
        private double JDate { get; set; }
        private int numIterations = 1;
        private int selectedAsrMethod;
        private int[] offset;
        private bool _useMoonSight = false;

        public PrayerTimeCalcBackup(bool useMonsight)
        {
            offset = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _useMoonSight = useMonsight;
            this.selectedAsrMethod = (int)ASRMETHOD.Shafii;
            //_setting = new DefaultSetting(10, 18, 0, 0, 18, ASRMETHOD.Shafii, HIGHLATMETHOD.OneSeventh,
            //    MIDHIGHTMETHOD.Standard, 0, 0);
            //setting = new DefaultSetting(10, 18, 0, 0, 17, ASRMETHOD.Shafii, HIGHLATMETHOD.NightMiddle, MIDHIGHTMETHOD.Standard, 0, 0);
            //_setting = new DefaultSetting(10, 16, 0, 4, 14, ASRMETHOD.Shafii, HIGHLATMETHOD.OneSeventh, MIDHIGHTMETHOD.Standard, 3, 0);
            _setting = new DefaultSetting(10, 18.5, 0, 0, 15, ASRMETHOD.Shafii, HIGHLATMETHOD.None, MIDHIGHTMETHOD.Standard, 0, 0);
        }

        public string[] getTimes(DateTime date, double latitude,
            double longitude, double timeZoneC, int dst)
        {
            this.DateTimeFormat = date;
            this.lat = latitude;
            this.lng = longitude;
            this.elv = dst;
            this.timeZone = timeZoneC;
            this.JDate = this.julian(date.Year, date.Month, date.Day) - longitude / (15 * 24);

            //return this.computeTimes();
            return this.computeTimes1();
        }



        private String[] computeTimes()
        {
            //i,faj,sr,dhu,Asr,ss,magr,ish,
            double[] times = { 5, 5, 6, 12, 13, 18, 18, 18 }; //default times
            //Times times=new Times(5, 5, 6, 12, 13, 18, 18, 18, 18);

            for (int i = 0; i < this.numIterations; i++)
            {
                times = this.computePrayerTimes(times);
                //times = this.computePrayerTimes(times);
            }

            times = this.adjustTimes(times);
            //this.adjustTimes(times);

            var midnight = (_setting.midnight == MIDHIGHTMETHOD.Jafari)
                ? times[5] + this.timeDiff(times[5], times[1]) / 2
                : times[5] + this.timeDiff(times[5], times[2]) / 2;

            var timesWithMidnight = new double[]
            {
                times[0], times[1], times[2], times[3], times[4], times[5],
                times[6], times[7], midnight
            };

            timesWithMidnight = this.tuneTimes(timesWithMidnight);
            //return this.modifyFormats(times);
            return this.adjustTimesFormat(timesWithMidnight);



            //return this.adjustTimesFormat(times);

        }

        private string[] computeTimes1()
        {
            Times times = new Times(5, 5, 6, 12, 13, 18, 18, 18, 18);

            for (int i = 0; i < this.numIterations; i++)
            {
                times = this.computePrayerTimes(times);
            }

            times = this.adjustTimes(times);

            times.Midnight = (_setting.midnight == MIDHIGHTMETHOD.Jafari)
                ? times.Sunset + this.timeDiff(times.Sunset, times.Fajr) / 2
                : times.Sunset + this.timeDiff(times.Sunset, times.Sunrise) / 2;

            times = this.tuneTimes(times);
            return this.adjustTimesFormat(times);
        }

        private double[] computePrayerTimes(double[] times)
        {
            times = this.dayPortion(times);
            var parameters = _setting;
            var imsak = this.sunAngleTime(parameters.imsakMinutes, times[0], CCW);
            var fajr = this.sunAngleTime(parameters.fajrAngle, times[1], CCW);
            var sunrise = this.sunAngleTime(this.riseSetAngle(), times[2], CCW);
            var dhuhr = this.midDay(times[3]);
            //var Asr = this.asrTime(this.asrFactor(parameters.Asr), times.Asr);
            var asr = this.asrTime(this.selectedAsrMethod, times[4]);
            var sunset = this.sunAngleTime(this.riseSetAngle(), times[5]);
            var maghrib = this.sunAngleTime(parameters.maghribMinutes, times[6]);
            var isha = this.sunAngleTime(parameters.ishaAngle, times[7]);

            return new[] { imsak, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha };
        }

        private Times computePrayerTimes(Times times)
        {
            times = this.dayPortion(times);
            var parameters = _setting;

            var sunrise = this.sunAngleTime(this.riseSetAngle(), times.Sunrise, CCW);
            var sunset = this.sunAngleTime(this.riseSetAngle(), times.Sunset);
            ;
            var fajr = _useMoonSight ? MoonSightFarj(sunrise) : this.sunAngleTime(parameters.fajrAngle, times.Fajr, CCW);
            var dhuhr = this.midDay(times.Dhuhr);
            var asr = this.asrTime(this.selectedAsrMethod, times.Asr);
            var maghrib = this.sunAngleTime(parameters.maghribAngle, times.Maghrib);
            var isha = _useMoonSight ? MoonSightIsha(sunset) : this.sunAngleTime(parameters.ishaAngle, times.Isha);

            return new Times(times.Imsak, fajr, sunrise, dhuhr, asr, sunset, maghrib, isha, 0.0);
        }

        private string[] adjustTimesFormat(double[] times)
        {
            String[] formatted = new String[times.Length];

            if (this.timeFormat == timeFormats.h_Float)
            {
                for (int i = 0; i < times.Length; ++i)
                {
                    formatted[i] = times[i] + "";
                }
                return formatted;
            }
            for (int i = 0; i < times.Length; i++)
            {
                if (this.timeFormat == timeFormats.h_12h)
                    formatted[i] = this.floatToTime12(times[i], true);
                else if (this.timeFormat == timeFormats.h_12hNS)
                    formatted[i] = this.floatToTime12NS(times[i]);
                else
                    formatted[i] = this.floatToTime24(times[i]);
            }
            return formatted;
        }

        private string[] adjustTimesFormat(Times times)
        {
            string[] formatted = new String[9];

            if (this.timeFormat == timeFormats.h_Float)
            {
                formatted[0] = times.Imsak.ToString(CultureInfo.InvariantCulture);
                formatted[1] = times.Fajr.ToString(CultureInfo.InvariantCulture);
                formatted[2] = times.Sunrise.ToString(CultureInfo.InvariantCulture);
                formatted[3] = times.Dhuhr.ToString(CultureInfo.InvariantCulture);
                formatted[4] = times.Asr.ToString(CultureInfo.InvariantCulture);
                formatted[5] = times.Sunset.ToString(CultureInfo.InvariantCulture);
                formatted[6] = times.Maghrib.ToString(CultureInfo.InvariantCulture);
                formatted[7] = times.Isha.ToString(CultureInfo.InvariantCulture);
                formatted[8] = times.Midnight.ToString(CultureInfo.InvariantCulture);

                return formatted;
            }
            if (this.timeFormat == timeFormats.h_12h)
            {
                formatted[0] = this.floatToTime12(times.Imsak, true);
                formatted[1] = this.floatToTime12(times.Fajr, true);
                formatted[2] = this.floatToTime12(times.Sunrise, true);
                formatted[3] = this.floatToTime12(times.Dhuhr, true);
                formatted[4] = this.floatToTime12(times.Asr, true);
                formatted[5] = this.floatToTime12(times.Sunset, true);
                formatted[6] = this.floatToTime12(times.Maghrib, true);
                formatted[7] = this.floatToTime12(times.Isha, true);
                formatted[8] = this.floatToTime12(times.Midnight, true);

            }
            else if (this.timeFormat == timeFormats.h_12hNS)
            {
                formatted[0] = this.floatToTime12NS(times.Imsak);
                formatted[1] = this.floatToTime12NS(times.Fajr);
                formatted[2] = this.floatToTime12NS(times.Sunrise);
                formatted[3] = this.floatToTime12NS(times.Dhuhr);
                formatted[4] = this.floatToTime12NS(times.Asr);
                formatted[5] = this.floatToTime12NS(times.Sunset);
                formatted[6] = this.floatToTime12NS(times.Maghrib);
                formatted[7] = this.floatToTime12NS(times.Isha);
                formatted[8] = this.floatToTime12NS(times.Midnight);

            }
            else
            {
                formatted[0] = this.floatToTime24(times.Imsak);
                formatted[1] = this.floatToTime24(times.Fajr);
                formatted[2] = this.floatToTime24(times.Sunrise);
                formatted[3] = this.floatToTime24(times.Dhuhr);
                formatted[4] = this.floatToTime24(times.Asr);
                formatted[5] = this.floatToTime24(times.Sunset);
                formatted[6] = this.floatToTime24(times.Maghrib);
                formatted[7] = this.floatToTime24(times.Isha);
                formatted[8] = this.floatToTime24(times.Midnight);
            }
            return formatted;
        }

        private double[] tuneTimes(double[] times)
        {
            for (int i = 0; i < times.Length; i++)
            {
                times[i] += offset[i] / 60.0;
            }
            return times;
        }

        private Times tuneTimes(Times times)
        {
            times.Imsak += offset[0] / 60.0;
            times.Fajr += offset[0] / 60.0;
            times.Sunrise += offset[0] / 60.0;
            times.Dhuhr += offset[0] / 60.0;
            times.Asr += offset[0] / 60.0;
            times.Sunset += offset[0] / 60.0;
            times.Maghrib += offset[0] / 60.0;
            times.Isha += offset[0] / 60.0;
            times.Midnight += offset[0] / 60.0;

            return times;
        }

        private double[] adjustTimes(double[] times)
        {
            var parameters = _setting;
            for (int i = 0; i < times.Length; i++)
            {
                times[i] += this.timeZone - this.lng / 15;
            }

            if (parameters.highLats != HIGHLATMETHOD.None)
                times = this.adjustHighLats(times);

            if (parameters.imsakMinutes >= 0)
                times[0] = times[1] - parameters.imsakMinutes / 60.0;
            if (parameters.maghribMinutes >= 0)
                times[6] = times[5] + parameters.maghribMinutes / 60.0;
            if (parameters.ishaMinutes > 0)
                times[7] = times[6] + parameters.ishaAngle / 60.0;
            times[3] += parameters.dhuhr / 60.0;

            return times;
        }

        private Times adjustTimes(Times times)
        {
            var parameters = _setting;

            times.Imsak += this.timeZone - this.lng / 15;
            times.Fajr += this.timeZone - this.lng / 15;
            times.Sunrise += this.timeZone - this.lng / 15;
            times.Dhuhr += this.timeZone - this.lng / 15;
            times.Asr += this.timeZone - this.lng / 15;
            times.Sunset += this.timeZone - this.lng / 15;
            times.Maghrib += this.timeZone - this.lng / 15;
            times.Isha += this.timeZone - this.lng / 15;

            if (parameters.highLats != HIGHLATMETHOD.None)
                times = this.adjustHighLats(times);

            if (parameters.imsakMinutes >= 0)
                times.Imsak = times.Fajr - parameters.imsakMinutes / 60.0;
            if (parameters.maghribMinutes >= 0 && Math.Abs(parameters.maghribAngle) < 0.001)
                 times.Maghrib = times.Sunset + parameters.maghribMinutes / 60.0;
            if (parameters.ishaMinutes > 0)
                times.Isha = times.Maghrib + parameters.ishaMinutes / 60.0;
            times.Dhuhr += parameters.dhuhr / 60.0;
            return times;


        }

        private double[] adjustHighLats(double[] times)
        {
            var parameters = _setting;
            var nightTime = this.timeDiff(times[5], times[2]);

            times[0] = this.adjustHLTime(times[0], times[2], parameters.imsakMinutes, nightTime, CCW);
            times[1] = this.adjustHLTime(times[1], times[2], parameters.fajrAngle, nightTime, CCW);
            times[7] = this.adjustHLTime(times[7], times[5], parameters.ishaAngle, nightTime);
            times[6] = this.adjustHLTime(times[6], times[5], parameters.maghribMinutes, nightTime);

            return times;
        }

        private Times adjustHighLats(Times times)
        {
            var parameters = _setting;
            var nightTime = this.timeDiff(times.Sunset, times.Sunrise);

            times.Imsak = this.adjustHLTime(times.Imsak, times.Sunrise, parameters.imsakMinutes, nightTime, CCW);
            times.Fajr = this.adjustHLTime(times.Fajr, times.Sunrise, parameters.fajrAngle, nightTime, CCW);
            times.Isha = this.adjustHLTime(times.Isha, times.Sunset, parameters.ishaAngle, nightTime);
            times.Maghrib = this.adjustHLTime(times.Isha, times.Sunset, parameters.maghribMinutes, nightTime);

            return times;
        }

        //time, base, angle, night, directio
        private double adjustHLTime(double time, double bases, double angle, double night, string direction = "NONE")
        {
            var portion = this.nightPortion(angle, night);
            var timeDiff = (direction == CCW)
                ? this.timeDiff(time, bases)
                : this.timeDiff(bases, time);
            if (double.IsNaN(time) || timeDiff > portion)
                time = bases + (direction == CCW ? -portion : portion);
            return time;
        }

        private double nightPortion(double angle, double night)
        {
            var method = _setting.highLats;
            var portion = 0.0;

            if (method == HIGHLATMETHOD.AngleBased)
                portion = 1.0 / 60.0 * angle;
            if (method == HIGHLATMETHOD.OneSeventh)
                portion = 1.0 / 7.0;
            if (method == HIGHLATMETHOD.NightMiddle)
                portion = 1.0 / 2.0;

            return portion * night;
        }



        private double sunAngleTime(double angle, double time, string direction = "NODIR")
        {
            var decl = this.sunPosition(JDate + time).declination;
            var noon = this.midDay(time);
            var t = (1 / 15.0) *
                    this.arccos((-this.dsin(angle) - this.dsin(decl) * this.dsin(lat)) / (this.dcos(decl) * this.dcos(lat)));
            return noon + (direction == CCW ? -t : t);
        }

        private sunAngleT sunPosition(double jd)
        {
            double D = jd - 2451545.0;
            double g = this.FixAngle(357.529 + 0.98560028 * D);
            double q = this.FixAngle(280.459 + 0.98564736 * D);
            double L = this.FixAngle(q + 1.915 * this.dsin(g) + 0.020 * this.dsin(2 * g));

            double R = 1.00014 - 0.01671 * this.dcos(g) - 0.00014 * this.dcos(2 * g);
            double e = 23.439 - 0.00000036 * D;
            var RA = this.arctan2(this.dcos(e) * this.dsin(L), this.dcos(L)) / 15;
            var eqt = q / 15 - this.FixHour(RA);
            var decl = this.darcsin(this.dsin(e) * this.dsin(L));

            return new sunAngleT(decl, eqt);

        }

        struct sunAngleT

        {
            public readonly double declination;
            public readonly double equation;

            public sunAngleT(double d, double e)
            {
                declination = d;
                equation = e;
            }
        }

        private double midDay(double time)
        {
            var eqt = this.sunPosition(JDate + time).equation;
            var noon = this.FixHour(12 - eqt);
            return noon;
        }

        private double asrTime(int factor, double time)
        {
            var decl = this.sunPosition(JDate + time).declination;
            var angle = -this.arccot(factor + this.dtan(Math.Abs(lat - decl)));
            return this.sunAngleTime(angle, time);
        }

        private double MoonSightIsha(double sunset)
        {
            var minutes = CalculateIshaMinimumGeneral(this.lat, this.DateTimeFormat);
            return sunset + (minutes / 60);
        }

        private double MoonSightFarj(double sunrise)
        {
            var minutes = CalculateFarjMinimumGeneral(this.lat, this.DateTimeFormat);
            return sunrise - (minutes / 60);
        }

        private double FixAngle(double angle)
        {
            return this.fix(angle, 360);
        }

        private double FixHour(double a)
        {
            return this.fix(a, 24);
        }

        private double fix(double a, int b)
        {
            a = a - b * (Math.Floor(a / b));
            return (a < 0) ? a + b : a;
        }



        private double[] dayPortion(double[] times)
        {
            for (int i = 0; i < times.Length; i++)
            {
                times[i] /= 24;
            }
            return times;
        }

        private Times dayPortion(Times times)
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

        private double julian(int year, int month, int day)
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

        private double dsin(double degree)
        {
            return Math.Sin(this.DegreeToRadian(degree));
        }

        private double dcos(double degree)
        {
            return Math.Cos(this.DegreeToRadian(degree));
        }

        private double dtan(double degree)
        {
            return Math.Tan(this.DegreeToRadian(degree));
        }

        private double darcsin(double degree)
        {
            return this.RadianToDegree(Math.Asin(degree));
        }

        private double arccos(double degree)
        {
            return this.RadianToDegree(Math.Acos(degree));
        }

        private double arctan(double degree)
        {
            return this.RadianToDegree(Math.Atan(degree));

        }

        private double arccot(double degree)
        {
            return this.RadianToDegree(Math.Atan(1 / degree));
        }

        private double arctan2(double y, double x)
        {
            return this.RadianToDegree(Math.Atan2(y, x));
        }

        private double DegreeToRadian(double degree)
        {
            return (degree * Math.PI) / 180.0;
        }

        private double RadianToDegree(double radian)
        {
            return (radian * 180.0) / Math.PI;
        }

        public void setAsrCalculationMethod(ASRMETHOD methodId)
        {
            this.selectedAsrMethod = (int)methodId;
        }

        private double riseSetAngle()
        {
            var angle = 0.0347 * Math.Sqrt(elv); // an approximation
            return 0.833 + angle;
        }

        private double timeDiff(double time1, double time2)
        {
            return this.FixHour(time2 - time1);
        }

        private String floatToTime24(double time)
        {
            if (time < 0)
                return invalidTime;
            time = this.FixHour(time + 0.5 / 60); // add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            return this.twoDigitsFormat((int)hours) + ":" + this.twoDigitsFormat((int)minutes);
        }

        // convert float hours to 12h format
        private String floatToTime12(double time, bool noSuffix)
        {
            if (time < 0)
                return invalidTime;
            time = this.FixHour(time + 0.5 / 60); // add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            String suffix = hours >= 12 ? " pm" : " am";
            hours = (hours + 12 - 1) % 12 + 1;
            return ((int)hours) + ":" + this.twoDigitsFormat((int)minutes) + (noSuffix ? "" : suffix);
        }

        private String twoDigitsFormat(int num)
        {

            return (num < 10) ? "0" + num : num + "";
        }

        // convert float hours to 12h format with no suffix
        private String floatToTime12NS(double time)
        {
            return this.floatToTime12(time, true);
        }

        private static double CalculateFarjMinimumGeneral(double lt, DateTime dateTime)
        {
            var A = 75 + (28.65 / 55) * Math.Abs(lt);
            var B = 75 + (19.44 / 55) * Math.Abs(lt);
            var C = 75 + (32.74 / 55) * Math.Abs(lt);
            var D = 75 + (48.1 / 55) * Math.Abs(lt);
            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);

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

        private static double CalculateIshaMinimumAhmer(double lt, DateTime dateTime)
        {
            var A = 62 + (17.4 / 55) * Math.Abs(lt);
            var B = 62 - (7.16 / 55) * Math.Abs(lt);
            var C = 62 + (5.12 / 55) * Math.Abs(lt);
            var D = 62 + (19.44 / 55) * Math.Abs(lt);
            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
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

        private static double CalculateIshaMinimumAbyad(double lt, DateTime dateTime)
        {
            var A = 75 + (25.6 / 55) * Math.Abs(lt);
            var B = 75 + (7.16 / 55) * Math.Abs(lt);
            var C = 75 + (36.84 / 55) * Math.Abs(lt);
            var D = 75 + (81.84 / 55) * Math.Abs(lt);
            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
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

        private static double CalculateIshaMinimumGeneral(double lt, DateTime dateTime)
        {
            double A = 75 + (25.6 / 55.0) * Math.Abs(lt);
            double B = 75 + (2.05 / 55.0) * Math.Abs(lt);
            double C = 75 - (9.21 / 55.0) * Math.Abs(lt);
            double D = 75 + (6.14 / 55.0) * Math.Abs(lt);
            var DYY = daysSinceSolstice(dateTime.DayOfYear, dateTime.Year, lt);
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

        static int daysSinceSolstice(int dayOfYear, int year, double latitude)
        {
            int daysSinceSolistice;
            int northernOffset = 10;
            bool isLeapYear = DateTime.IsLeapYear(year);
            int southernOffset = isLeapYear ? 173 : 172;
            int daysInYear = isLeapYear ? 366 : 365;

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