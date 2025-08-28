
using System.Security.Cryptography;
using Flights.Domain.Errors;

namespace Flights.Domain.Entities
{
    public record Flight(
        Guid Id,
        string Airline,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingSeats,
        string price
    )
    {
        public IList<Booking> Bookings = new List<Booking>();
        public int RemainingSeats { get; set; } = RemainingSeats;
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