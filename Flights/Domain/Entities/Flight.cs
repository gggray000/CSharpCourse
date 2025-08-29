
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Flights.Domain.Errors;

namespace Flights.Domain.Entities
{
    public class Flight
    {

        public Guid Id { get; set; }
        public string Airline { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingSeats { get; set; }
        public string Price { get; set; }

        public IList<Booking> Bookings = new List<Booking>();

        public Flight(
            Guid id,
            string airline,
            TimePlace departure,
            TimePlace arrival,
            int remainingSeats,
            string price
        )
        {
            Id = id;
            Airline = airline;
            Departure = departure;
            Arrival = arrival;
            RemainingSeats = remainingSeats;
            Price = price;
        }

        public Flight() { }

        internal object? AddBooking(string email, byte numberOfSeats)
        {
            if (this.RemainingSeats < numberOfSeats)
                return new OverbookError();

            this.Bookings.Add(
                new Booking(
                    email,
                    numberOfSeats
                )
            );

            this.RemainingSeats -= numberOfSeats;
            return null;
        }
    }
}