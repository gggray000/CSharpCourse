
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
    }
}