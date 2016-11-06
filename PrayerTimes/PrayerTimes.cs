using System;
using PrayerTimes.Types;
using PrayerTimes.Utilities;

namespace PrayerTimes
{
    public class PrayerTimes
    {
        public string ImsaNkHour => _imsak.DoubleToString(this._dateComponent.TimeFormat);
        public string FajrHour => _fajr.DoubleToString(this._dateComponent.TimeFormat);
        public string SunriseHour => _sunrise.DoubleToString(this._dateComponent.TimeFormat);
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
        private readonly AstronomicalCalculations _astronomicalCalculation;
        private int _numIterations = 1;

        public PrayerTimes(DateComponent dateComponent, Coordinates coordinates, CalculationMethods calculationMethod)
        {
            this.SetStartTimesForCalculation();
            this._dateComponent = dateComponent;
            this._coordinates = coordinates;
            this._calculationParameters = CalculationParameters.SetCalculationMethod(calculationMethod);
            this._jDate = TimeUtilities.Julian(dateComponent.CalculationDate.Year, dateComponent.CalculationDate.Month, dateComponent.CalculationDate.Day) - coordinates.Longtitud / (15 * 24);

            _astronomicalCalculation = new AstronomicalCalculations(calculationMethod);
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

            _sunrise = _astronomicalCalculation.SunAngleTime(_astronomicalCalculation.RiseSetAngle(_coordinates.Elevation), this._sunrise, _jDate, _coordinates.Latitude, isCcwDirection: true);
            _sunset = _astronomicalCalculation.SunAngleTime(_astronomicalCalculation.RiseSetAngle(_coordinates.Elevation), this._sunset, _jDate, _coordinates.Latitude);
            _fajr = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? _astronomicalCalculation.MoonSightFarj(_sunrise, _coordinates.Latitude, _dateComponent.CalculationDate) : _astronomicalCalculation.SunAngleTime(_calculationParameters.FajrAngle, _fajr, _jDate, _coordinates.Latitude, isCcwDirection: true);
            _dhuhr = _astronomicalCalculation.MidDay(this._dhuhr, _jDate);
            _asr = _astronomicalCalculation.AsrTime((int)(_calculationParameters.AsrMethodCalculation) + 1, this._asr, _jDate, _coordinates.Latitude);
            _maghrib = _astronomicalCalculation.SunAngleTime(_calculationParameters.MaghribAngle, this._maghrib, _jDate, _coordinates.Latitude);
            _isha = CalculationParameters.SelectedCalculationMethod == CalculationMethods.MOON_SIGHTING_COMMITTEE ? _astronomicalCalculation.MoonSightIsha(_sunset, _coordinates.Latitude, _dateComponent.CalculationDate) : _astronomicalCalculation.SunAngleTime(_calculationParameters.IshaAngle, _isha, _jDate, _coordinates.Latitude);
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
            this._imsak += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._fajr += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._sunrise += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._dhuhr += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._asr += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._sunset += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._maghrib += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
            this._isha += _dateComponent.TimeZone - _coordinates.Longtitud / 15;
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
    }
}
