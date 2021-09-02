using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FlightPlanner.Classes;
using FlightPlanner.DbContext;
using FlightPlanner.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddFlightRequest = FlightPlanner.DbModels.AddFlightRequest;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("admin-api/flights")]
    [Authorize]
    [SuppressMessage("ReSharper", "CA1822")]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public DbSet<Flight> ReturnFlights()
        {
            var flights = _context.Flights;
            
            return flights;
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Flight> ReturnFlight(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            
            if (flight != null)
            {
                return flight;
            }

            return NotFound();
            
            /*var flight = FlightsStorage.GetFlightById(id);

            if (flight != null)
            {
                return flight;
            }*/
            
            return NotFound();
        }

        [HttpPut]
        public IActionResult AddFlightRequest(AddFlightRequest request)
        {
            // done
            if (!FlightsStorage.IsValidFlight(request))
            {
                return BadRequest();
            }

            // done
            if (FlightsStorage.IsSameAirport(request))
            {
                return BadRequest();
            }

            // done
            if (FlightsStorage.IsStrangeDate(request))
            {
                return BadRequest();
            }
            
            if (FlightsStorage.IsInFlightsList(request))
            {
                return Conflict(request);
            }

            /*var fromAirport = request.From;
            AirportsStorage.AddAirport(fromAirport);
            
            var toAirport = request.To;
            AirportsStorage.AddAirport(toAirport);*/

            // var added = FlightsDbStorage.AddFlight(flight);
            
            var flight = new Flight
            {
                From = new Airport
                {
                    Country = request.From.Country,
                    City = request.From.City,
                    AirportCode = request.From.AirportCode
                },
                To = new Airport
                {
                    Country = request.To.Country,
                    City = request.To.City,
                    AirportCode = request.To.AirportCode
                },
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                Carrier = request.Carrier
            };

            _context.Add(flight);

            _context.SaveChanges();
            
            return Created("", flight);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlightRequest(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}