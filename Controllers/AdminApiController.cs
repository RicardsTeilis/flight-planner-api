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
        public ActionResult<Flight> ReturnFlight(int id)
        {
            var flight = FlightsStorage.GetFlightById(id);

            if (flight != null)
            {
                return flight;
            }
            
            return NotFound();
        }

        [HttpPut]
        public IActionResult AddFlightRequest(AddFlightRequest flight)
        {
            if (!FlightsStorage.IsValidFlight(flight))
            {
                return BadRequest();
            }

            if (FlightsStorage.IsSameAirport(flight))
            {
                return BadRequest();
            }

            if (FlightsStorage.IsStrangeDate(flight))
            {
                return BadRequest();
            }
            
            if (FlightsStorage.IsInFlightsList(flight))
            {
                return Conflict(flight);
            }

            var fromAirport = flight.From;
            AirportsStorage.AddAirport(fromAirport);
            
            var toAirport = flight.To;
            AirportsStorage.AddAirport(toAirport);
            
            var added = FlightsStorage.AddFlight(flight);
            
            return Created("", added);

        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlightRequest(int id)
        {
            FlightsStorage.DeleteFlight(id);
            return Ok();
        }
    }
}