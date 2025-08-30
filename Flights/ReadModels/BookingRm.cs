namespace Flights.ReadModels
{
    public record BookingRm(
        Guid FlightId,
        string Airline,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int NumberOfBookedSeats,
        string Price,
        string email
    );
}