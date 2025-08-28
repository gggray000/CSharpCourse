using System.ComponentModel.DataAnnotations;

namespace Flights.Domain.Entities
{
    public record Booking(
        string Email,
        byte NumberOfSeats
    );
}