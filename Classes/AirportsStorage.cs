using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.DbModels;

namespace FlightPlanner.Classes
{
    public static class AirportsStorage
    {
        private static readonly List<Airport> Airports;

        static AirportsStorage()
        {
            Airports = new List<Airport>();
        }

        public static void ClearAirportsList()
        {
            Airports.Clear();
        }

        public static void AddAirport(Airport airport)
        {
            if (!Airports.Contains(airport))
            {
                Airports.Add(airport);
            }
        }

        public static Airport[] GetAirport(string search)
        {
            var result = Airports.Find(a => a.City.ToLower().StartsWith(search.ToLower().Replace(" ", string.Empty)) || a.Country.ToLower().StartsWith(search.ToLower().Replace(" ", string.Empty)) || a.AirportCode.ToLower().StartsWith(search.ToLower().Replace(" ", string.Empty)));

            return new[]
            {
                result
            };
        }
    }
}