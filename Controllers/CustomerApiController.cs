using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FlightPlanner.Classes;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}