using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Flights.Data
{
    // Mock Database
    public class Entities : DbContext
    {
        public Entities(DbContextOptions<Entities> options) : base(options)
        {        }

        // Passenger needs a primary key.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
            modelBuilder.Entity<Flight>().Property(f => f.RemainingSeats).IsConcurrencyToken();
            modelBuilder.Entity<Flight>().OwnsMany(f => f.Bookings);
        }
        static Random random = new Random();
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();
        

    }
}