using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FlightPlanner.Classes;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("admin-api/flights")]
    [Authorize]
    [SuppressMessage("ReSharper", "CA1822")]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        public List<Flight> ReturnFlights()
        {
            return FlightsStorage.GetFlights();
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public Flight ReturnFlight(int id)
        {
            var flight = FlightsStorage.GetFlightById(id);

            return flight;
        }

        [HttpPut]
        public IActionResult AddFlightRequest(AddFlightRequest flight)
        {
            var added = FlightsStorage.AddFlight(flight);
            
            return Created("", added);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void DeleteFlightRequest(int id)
        {
            FlightsStorage.DeleteFlight(id);
        }
    }
}