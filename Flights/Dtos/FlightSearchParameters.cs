using System.ComponentModel;

namespace Flights.Dtos
{
    public record FlightsSearchParameters(
        [DefaultValue("Guangzhou")]
        string? From,
        [DefaultValue("Frankfurt")]
        string? To,
        [DefaultValue("12/25/2025 10:30:00 AM")]
        DateTime? FromDate,
        [DefaultValue("12/26/2025 10:30:00 AM")]
        DateTime? ToDate,
        [DefaultValue(1)]
        int? NumberOfPassengers
    );
}