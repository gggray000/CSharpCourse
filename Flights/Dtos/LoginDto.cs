using System.ComponentModel.DataAnnotations;

namespace Flights.Dtos
{
    public record LoginDto(
        [Required][EmailAddress][StringLength(100, MinimumLength = 5)] string email,
        [Required][StringLength(30, MinimumLength = 6)] string password
    );
}