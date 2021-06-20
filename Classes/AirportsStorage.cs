using System.Collections.Generic;
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

        public static List<Airport> GetAirports()
        {
            return Airports;
        }
    }
}