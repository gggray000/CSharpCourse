using Flights.Domain.Entities;

namespace Flights.Data
{
    // Mock Database
    public class Entities
    {
        static Random random = new Random();
        public IList<Passenger> Passengers = new List<Passenger>();
        public List<Flight> Flights = new List<Flight>();
    }
}