using FlightPlanner.Classes;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class TestingApiController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            FlightsStorage.ClearFlightsList();
            AirportsStorage.ClearAirportsList();
            
            return Ok();
        }
    }
}