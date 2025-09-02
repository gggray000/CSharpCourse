using Microsoft.AspNetCore.Mvc;
using Flights.ReadModels;
using Flights.Domain.Entities;
using Flights.Dtos;
using Flights.Domain.Errors;
using Flights.Data;
using Microsoft.EntityFrameworkCore;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly Entities _entities;
        private readonly ILogger<FlightController> _logger;

        public FlightController(
            ILogger<FlightController> logger,
            Entities entities
            )
        {
            _logger = logger;
            _entities = entities;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search([FromQuery] FlightsSearchParameters @params)
        {
            _logger.LogInformation("Searching for flights to: {To}", @params.To);

            IQueryable<Flight> flights = _entities.Flights;

            if (!string.IsNullOrWhiteSpace(@params.From))
                flights = flights
                    .Where(flight => flight.Departure.place.Contains(@params.From));

            if (!string.IsNullOrWhiteSpace(@params.To))
                flights = flights
                    .Where(flight => flight.Arrival.place.Contains(@params.To));

            if (@params.FromDate != null)
                flights = flights
                    .Where(flight => flight.Departure.time >= @params.FromDate.Value.Date);

            if (@params.ToDate != null)
                flights = flights
                    .Where(flight => flight.Departure.time >= @params.ToDate.Value.Date.AddDays(1).AddTicks(-1));

            if (@params.NumberOfPassengers != null && @params.NumberOfPassengers != 0)
                flights = flights
                    .Where(flight => flight.RemainingSeats >= @params.NumberOfPassengers);
            else
                flights = flights
                    .Where(flight => flight.RemainingSeats >= 1);

            var flightRmList = flights
            .Select(flight =>
                new FlightRm(
                        flight.Id,
                        flight.Airline,
                        new TimePlaceRm(
                            flight.Departure.place.ToString(),
                            flight.Departure.time),
                        new TimePlaceRm(
                            flight.Arrival.place.ToString(),
                            flight.Arrival.time),
                        flight.RemainingSeats,
                        flight.Price
                    ))
            .ToArray();

            return flightRmList;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);
            if (flight == null)
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                new TimePlaceRm(
                    flight.Departure.place.ToString(),
                    flight.Departure.time),
                new TimePlaceRm(
                    flight.Arrival.place.ToString(),
                    flight.Arrival.time),
                flight.RemainingSeats,
                flight.Price
                );
            return Ok(readModel);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Book(BookDto dto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);
            if (flight == null)
                return NotFound();

            var error = flight.AddBooking(dto.Email, dto.NumberOfSeats);
            if (error is OverbookError)
                return Conflict(new { message = "Not enough remaining seats." });

            try
            {
                _entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Conflict(new { message = "Error happened." });
            }

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId }, dto);

        }



    }
}