using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;

namespace FlightPlanner.Classes
{
    public static class AirportsStorage
    {
        private static readonly List<Airport> Airports;

        static AirportsStorage()
        {
            Airports = new List<Airport>();
        }

        public static void AddAirport(Airport airport)
        {
            Airports.Add(airport);
        }

        public static Airport[] GetAirport(string search)
        {
            var result = Airports.Find(a => a.City.ToLower().StartsWith(search.ToLower()) || a.Country.ToLower().StartsWith(search.ToLower()) || a.AirportCode.ToLower().StartsWith(search.ToLower()));

            return new[]
            {
                result
            };
        }
    }
}