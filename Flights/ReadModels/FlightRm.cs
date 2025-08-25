
namespace Flights.ReadModels
{
    public record FlightRm (
        Guid Id,
        string Airline,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int RemainingSeats,
        string price
    );
}