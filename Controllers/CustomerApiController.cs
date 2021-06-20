using System.Collections.Generic;
using FlightPlanner.Classes;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public List<Airport> ReturnAirports()
        {
            return AirportsStorage.GetAirports();
        }
    }
}