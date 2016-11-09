﻿namespace PrayerTimes.Types
{
    public class Coordinates
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public double Elevation { get; private set; }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
            Elevation = 0;
        }
        public Coordinates(double lat, double lon, int elevation) : this(lat, lon)
        {
            Elevation = elevation;
        }
    }
}
