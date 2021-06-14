﻿using System;

namespace FlightPlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}