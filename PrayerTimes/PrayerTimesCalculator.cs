using System;
using PrayerTimes.Types;
using PrayerTimes.Utilities;

namespace PrayerTimes
{
    public class PrayerTimesCalculator: IPrayerTimesCalculator
    {
        #region variables
        private double _imsak;
        private double _fajr;
        private double _sunrise;
        private double _dhuhr;
        private double _asr;
        private double _sunset;
        private double _maghrib;
        private double _isha;
        private double _midnight;
        private double _jDate;
        private DateComponent _dateComponent;
        private readonly Coordinates _coordinates;
        private readonly CalculationParameters _calculationParameters;
        private readonly AstronomicalCalculations _astronomicalCalculation;
        private int _numIterations = 1;
        #endregion

        #region Constructors

        public PrayerTimesCalculator(Coordinates coordinates, CalculationMethods calculationMethod)
        {
            this._coordinates = coordinates;
            this._calculationParameters = new CalculationParameters(calculationMethod);
            if (this._coordinates.Latitude > 55.0)
                this._calculationParameters.SetHighLatituteRule(HighLatitudeRule.OneSeventh);

            this._astronomicalCalculation =
                new AstronomicalCalculations(_calculationParameters.SelectedCalculationMethod);
        }

        public PrayerTimesCalculator(Coordinates coordinates, CalculationParameters calculationParameters)
        {
            this._coordinates = coordinates;
            this._calculationParameters = calculationParameters;
            if (this._coordinates.Latitude > 55.0)
                this._calculationParameters.SetHighLatituteRule(HighLatitudeRule.OneSeventh);

            this._astronomicalCalculation = new AstronomicalCalculations(CalculationMethods.OTHER);
        }
        #endregion

        public PrayerHours GetPrayerTimesForDate(DateComponent dateComponent)
        {
            this.SetStartTimesForCalculation();
            this.SetCalculationDate(dateComponent);
            this.ComputeTimes();
            return new PrayerHours(this._imsak,this._fajr,this._sunrise,this._dhuhr,this._asr,this._sunset,this._maghrib,this._isha,this._midnight,this._dateComponent.TimeFormat);
        }
        #region Private methods
        private void SetCalculationDate(DateComponent dateComponent)
        {
            this._dateComponent = dateComponent;
            this._jDate = TimeUtilities.Julian(dateComponent.CalculationDate.Year, dateComponent.CalculationDate.Month, dateComponent.CalculationDate.Day) - this._coordinates.Longitude / (15 * 24);
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

            //var safeLatitude = double.IsNaN(_coordinates.SafeLatitude)
            //    ? _coordinates.Latitude
            //    : _coordinates.SafeLatitude;

             var safeLatitude = _coordinates.Latitude;

            _sunrise = _astronomicalCalculation.SunAngleTime(_astronomicalCalculation.RiseSetAngle(_coordinates.Elevation), this._sunrise, _jDate, safeLatitude, isCcwDirection: true);
            _sunset = _astronomicalCalculation.SunAngleTime(_astronomicalCalculation.RiseSetAngle(_coordinates.Elevation), this._sunset, _jDate, safeLatitude);

            _fajr = _calculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? _astronomicalCalculation.MoonSightFarj(_sunrise, safeLatitude, _dateComponent.CalculationDate) 
                : _astronomicalCalculation.SunAngleTime(_calculationParameters.FajrAngle, _fajr, _jDate, safeLatitude, isCcwDirection: true);
            _dhuhr = _astronomicalCalculation.MidDay(this._dhuhr, _jDate);

            //adding 1 to asrmethod for shadow lenght where 1 is one lenght and 2 is double shadow lenght for Hanafi
            _asr = _astronomicalCalculation.AsrTime((int)(_calculationParameters.AsrMethodCalculation) + 1, this._asr, _jDate, safeLatitude);
            _maghrib = _astronomicalCalculation.SunAngleTime(_calculationParameters.MaghribAngle, this._maghrib, _jDate, safeLatitude);
            _isha = _calculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? _astronomicalCalculation.MoonSightIsha(_sunset, safeLatitude, _dateComponent.CalculationDate) 
                : _astronomicalCalculation.SunAngleTime(_calculationParameters.IshaAngle, _isha, _jDate, safeLatitude);
            _midnight = (_calculationParameters.MidnightMethod == MidnightMethod.Jafari) ? _sunset + TimeUtilities.TimeDiff(_sunset, _fajr) / 2 : _sunset + TimeUtilities.TimeDiff(_sunset, _sunrise) / 2;
            
            //We set default time for Imsak equal to Fajr
            _imsak = _fajr;

        }

        private void TuneTimes()
        {
            this._fajr += _calculationParameters.PrayerTimeAdjustments.Fajr / 60.0;
            this._imsak = this._fajr + _calculationParameters.PrayerTimeAdjustments.Imsak / 60.0;
            this._dhuhr += _calculationParameters.PrayerTimeAdjustments.Dhuhr / 60.0;
            this._asr += _calculationParameters.PrayerTimeAdjustments.Asr / 60.0;
            this._maghrib += _calculationParameters.PrayerTimeAdjustments.Maghrib / 60.0;
            this._isha += _calculationParameters.PrayerTimeAdjustments.Isha / 60.0;
        }

        private void AdjustTimes()
        {
            if (_calculationParameters.HighLatituteRule != HighLatitudeRule.None)
                this.AdjustHighLats();

            if (Math.Abs(_calculationParameters.MaghribAngle) < 0.001)
                this._maghrib = this._sunset + _calculationParameters.MaghribAngle / 60.0;
            if (_calculationParameters.IshaIntervalAfterMaghrib > 0)
                this._isha = this._maghrib + _calculationParameters.IshaIntervalAfterMaghrib / 60.0;
        }

        private void AjdustTimeZone()
        {
            this._imsak += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._fajr += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._sunrise += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._dhuhr += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._asr += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._sunset += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._maghrib += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
            this._isha += _dateComponent.TimeZoneUtcOffset - _coordinates.Longitude / 15;
        }

        private void AdjustHighLats()
        {
            var nightTime = TimeUtilities.TimeDiff(this._sunset, this._sunrise);

            this._fajr = _astronomicalCalculation.AdjustTimeForHighLatitutes(this._fajr, this._sunrise, _calculationParameters.FajrAngle, nightTime, _calculationParameters.HighLatituteRule, isCcwDirection: true);
            this._isha = _astronomicalCalculation.AdjustTimeForHighLatitutes(this._isha, this._sunset, _calculationParameters.IshaAngle, nightTime, _calculationParameters.HighLatituteRule);
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

        #endregion
    }

    public interface IPrayerTimesCalculator
    {
        PrayerHours GetPrayerTimesForDate(DateComponent dateComponent);
    }
}
