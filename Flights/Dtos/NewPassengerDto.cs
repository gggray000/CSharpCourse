using System.ComponentModel.DataAnnotations;

namespace Flights.Dtos
{
    public record NewPassengerDto(
        [Required][EmailAddress][StringLength(100, MinimumLength = 5)] string email,
        [Required][StringLength(30, MinimumLength = 6)] string password,
        [Required][StringLength(30, MinimumLength = 2)] string firstName,
        [Required][StringLength(30, MinimumLength = 2)] string lastName,
        [Required] bool gender
    );
}