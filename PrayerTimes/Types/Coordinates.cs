using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayerTimes.Types
{
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
}
