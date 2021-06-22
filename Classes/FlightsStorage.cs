using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;

namespace FlightPlanner.Classes
{
    public static class FlightsStorage
    {
        private static readonly List<Flight> Flights;
        private static int _flightId;

        static FlightsStorage()
        {
            Flights = new List<Flight>();
            _flightId = 1;
        }

        public static void ClearFlightsList()
        {
            Flights.Clear();
        }
        
        public static List<Flight> GetFlights()
        {
            return Flights;
        }

        public static Flight GetFlightById(int id)
        {
            return Flights.Exists(i => i.Id == id) ? Flights.Find(i => i.Id == id) : null;
        }

        public static Flight SearchFlight(SearchFlightsRequest search)
        {
            if (Flights.Any(i => i.From.AirportCode == search.From &&
                                      i.To.AirportCode == search.To &&
                                      i.DepartureTime.Substring(0,10) == search.DepartureDate))
            {
                var flight = Flights.FirstOrDefault(i => i.From.AirportCode == search.From &&
                                                         i.To.AirportCode == search.To &&
                                                         i.DepartureTime.Substring(0,10) == search.DepartureDate);
                return flight;
            }

            return null;
        }

        public static Flight AddFlight(AddFlightRequest newFlight)
        {
            var flight = new Flight
            {
                Id = _flightId,
                From = new Airport
                {
                    Country = newFlight.From.Country,
                    City = newFlight.From.City,
                    AirportCode = newFlight.From.AirportCode
                },
                To = new Airport
                {
                    Country = newFlight.To.Country,
                    City = newFlight.To.City,
                    AirportCode = newFlight.To.AirportCode
                },
                Carrier = newFlight.Carrier,
                DepartureTime = newFlight.DepartureTime,
                ArrivalTime = newFlight.ArrivalTime
            };

            Flights.Add(flight);
            _flightId++;
            return flight;
        }

        public static bool IsValidFlight(AddFlightRequest flight)
        {
            if (flight.From == null ||
                string.IsNullOrEmpty(flight.From?.Country) ||
                string.IsNullOrEmpty(flight.From?.City) ||
                string.IsNullOrEmpty(flight.From?.AirportCode) ||
                flight.To == null ||
                string.IsNullOrEmpty(flight.To?.Country) ||
                string.IsNullOrEmpty(flight.To?.City) ||
                string.IsNullOrEmpty(flight.To?.AirportCode) ||
                string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.DepartureTime) ||
                string.IsNullOrEmpty(flight.ArrivalTime))
            {
                return false;
            }

            return true;
        }

        public static bool IsSameAirport(AddFlightRequest flight)
        {
            return string.Equals(flight.From.AirportCode.Replace(" ", string.Empty), flight.To.AirportCode.Replace(" ", string.Empty), StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsInFlightsList(AddFlightRequest newFlight)
        {
            foreach (var flight in Flights)
            {
                if (flight.From.AirportCode == newFlight.From.AirportCode &&
                    flight.From.Country == newFlight.From.Country &&
                    flight.From.City == newFlight.From.City &&
                    flight.To.AirportCode == newFlight.To.AirportCode &&
                    flight.To.Country == newFlight.To.Country &&
                    flight.To.City == newFlight.To.City &&
                    flight.Carrier == newFlight.Carrier &&
                    flight.DepartureTime == newFlight.DepartureTime &&
                    flight.ArrivalTime == newFlight.ArrivalTime)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsStrangeDate(AddFlightRequest flight)
        {
            if (DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime))
            {
                return true;
            }
            
            return false;
        }

        public static void DeleteFlight(int id)
        {
            var isFlightInList = Flights.Any(i => i.Id == id);

            if (isFlightInList)
            {
                var flight = GetFlightById(id);
                Flights.Remove(flight);
            }
        }
    }
}