using Microsoft.AspNetCore.Mvc;
using Flights.ReadModels;
using Flights.Data;
using Flights.Dtos;
using Flights.Domain.Errors;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities)
        {
            _entities = entities;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<BookingRm>> List(string email)
        {
            IEnumerable<BookingRm> bookings = _entities.Flights.ToArray()
                .SelectMany(flight => flight.Bookings
                    .Where(booking => booking.Email == email)
                    .Select(booking => new BookingRm(
                        flight.Id,
                        flight.Airline,
                        new TimePlaceRm(flight.Departure.place, flight.Departure.time),
                        new TimePlaceRm(flight.Arrival.place, flight.Arrival.time),
                        booking.NumberOfSeats,
                        flight.Price,
                        email
                    )));

            return Ok(bookings);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Cancel(BookDto dto)
        {
            var flight = _entities.Flights.Find(dto.FlightId);
            var error = flight?.CancelBooking(dto.Email, dto.NumberOfSeats);
            if (error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }
            else if (error is NotFoundError)
            {
                return NotFound();
            }
            else
            {
                throw new Exception($"The error type: {error.GetType().Name} occurred while canceling booking.");
            }
        }

    }
}