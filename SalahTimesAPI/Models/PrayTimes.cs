using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalahTimesAPI.Models
{
    public class PrayTimes
    {
        public string Imsak { get; set; }
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhur { get; set; }
        public string Asr { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Midnight { get; set; }
    }
}
