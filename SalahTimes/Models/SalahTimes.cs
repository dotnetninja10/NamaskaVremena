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
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Alt { get; set; }
        public int CalculationMethod { get; set; }
        public int HighLatituteMethod { get; set; }
        public int AsrMethod { get; set; }
        public int MidnightMethod { get; set; }
        public short Imsak { get; set; }
        public short Fajr { get; set; }
        public short Dhuhr { get; set; }
        public short Asr { get; set; }
        public short Maghrib { get; set; }
        public short Isha { get; set; }
        public int Tz { get; set; }
        public SalahTimesOptions()
        {
            CalculationMethod = -1;
        }
        
        
        
    }

    public class SalahTimesOptionsForCustomAngles : SalahTimesOptions
    {
        public double FajrAngle { get; set; }
        public double IshaAngle { get; set; } 
    }
}
