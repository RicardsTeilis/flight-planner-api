using FlightPlanner.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.DbContext
{
    public class FlightPlannerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public FlightPlannerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}