using System.ComponentModel.DataAnnotations;

namespace Flights.Domain.Entities
{
    public record Passenger(
        string email,
        string firstName,
        string lastName,
        bool gender
    );
}