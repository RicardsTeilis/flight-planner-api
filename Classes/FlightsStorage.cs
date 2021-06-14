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
        
        public static List<Flight> GetFlights()
        {
            return Flights;
        }

        public static Flight GetFlightById(int id)
        {
            var flight = Flights.Find(i => i.Id == id);
            
            return flight;
        }

        public static Flight AddFlight(AddFlightRequest addRequest)
        {
            var flight = new Flight
            {
                Id = _flightId,
                From = addRequest.From,
                To = addRequest.To,
                Carrier = addRequest.Carrier,
                DepartureTime = addRequest.DepartureTime,
                ArrivalTime = addRequest.ArrivalTime
            };
            
            Flights.Add(flight);
            _flightId++;
            
            return flight;
        }

        public static void DeleteFlight(int id)
        {
            Flights.Remove(Flights.Single(i => i.Id == id));
        }
    }
}