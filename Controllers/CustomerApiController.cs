using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FlightPlanner.Classes;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Array = System.Array;
using PageResult = FlightPlanner.Models.PageResult;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("api")]
    [SuppressMessage("ReSharper", "CA1822")]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public Airport[] ReturnAirport(string search)
        {
            return AirportsStorage.GetAirport(search);
        }

        [HttpPost]
        [Route("flights/search")]
        public ActionResult<PageResult> ReturnFlights(SearchFlightsRequest search)
        {
            if (string.IsNullOrEmpty(search.From) || string.IsNullOrEmpty(search.To) ||
                string.IsNullOrEmpty(search.DepartureDate))
            {
                return BadRequest();
            }

            if (string.Equals(search.From, search.To, StringComparison.CurrentCultureIgnoreCase))
            {
                return BadRequest();
            }

            var flight = FlightsStorage.SearchFlight(search);

            if (flight != null)
            {
                var result = new PageResult
                {
                    Page = 0,
                    TotalItems = 1,
                    Items = new List<Flight>
                    {
                        flight
                    }
                };

                return result;
            }
            
            return new PageResult
            {
                Page = 0,
                TotalItems = 0,
                Items = new List<Flight>()
            };
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public ActionResult<Flight> ReturnFlightById(int id)
        {
            var flight = FlightsStorage.GetFlightById(id);

            if (flight != null)
            {
                return flight;
            }
            
            return NotFound();
        }
    }
}