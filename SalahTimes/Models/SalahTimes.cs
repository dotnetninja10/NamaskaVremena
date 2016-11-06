using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalahTimes.Models
{
    public class SalahTimes
    {
        public string Date { get; set; }
        public string Imsak { get; set; }
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhur { get; set; }
        public string Asr { get; set; }
        public string Sunset { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Midnight { get; set; }
    }

    public class SalahTimesOptions
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CalculationMethod { get; set; }
        public int HighLatituteMethod { get; set; }
        public int AsrMethod { get; set; }
        public int Altitude { get; set; }
        public int ImsakMinutesBeforeFajr { get; set; }
        public int DhurMinutesAfterMidDay { get; set; }
        public int MaghribMinutesAfterSunset { get; set; }
        public int IshaMinutesAfterMaghrib { get; set; }
       
    }

    public class SalahTimesOptionsForCustomAngles : SalahTimesOptions
    {
        public double FajrAngle { get; set; }
        public double IshaAngle { get; set; } 
    }
}
