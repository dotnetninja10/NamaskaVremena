using System.Security.AccessControl;

namespace PrayerTimes.Types
{
    public class Coordinates
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double SafeLatitude { get; private set; }
        public double Elevation { get; private set; }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
            Elevation = 0;
            SafeLatitude = double.NaN;
            if (lat > 60.0)
                SafeLatitude = 60.0;
            if (lat < -60.0)
                SafeLatitude = -60.0;

        }
        public Coordinates(double lat, double lon, int elevation) : this(lat, lon)
        {
            if (elevation < 0)
                Elevation = 0;
            
            Elevation = elevation;
        }
    }
}
