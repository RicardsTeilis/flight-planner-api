using FlightPlanner.Classes;
using FlightPlanner.DbContext;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class TestingApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public TestingApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            var flights = _context.Flights;
            
            _context.Flights.RemoveRange(flights);
            _context.SaveChanges();
            
            var airports = _context.Airports;
            
            _context.Airports.RemoveRange(airports);
            _context.SaveChanges();
            
            /*FlightsStorage.ClearFlightsList();
            AirportsStorage.ClearAirportsList();*/
            
            return Ok();
        }
    }
}