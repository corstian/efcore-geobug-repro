using GeoAPI.Geometries;
using System;
using System.Collections.ObjectModel;

namespace efcore_spatial_aggregateException
{
    public class Airfield
    {
        public Guid Id { get; set; }
        public int? OurAirfieldsId { get; set; }
        public string Name { get; set; }
        public string Icao { get; set; }
        public string Iata { get; set; }
        public string Type { get; set; }
        public string HomePage { get; set; }
        public IPoint Location { get; set; }

        public string Continent { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Muncipality { get; set; }

        public bool HasScheduledService { get; set; }
    }
}
