using Microsoft.AspNetCore.Mvc;
using Flights.ReadModels;
using Flights.Domain.Entities;
using Flights.Dtos;
using Flights.Domain.Errors;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        static Random random = new Random();

        private static Flight[] flights = new Flight[]
        {
        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 3))),
            new TimePlace("Vienna",DateTime.Now.AddHours(random.Next(4, 10))),
            2,
            random.Next(2000, 5000).ToString()
        ),

        new (
            Guid.NewGuid(),
            "Lufthansa",
            new TimePlace("Berlin",DateTime.Now.AddHours(random.Next(1, 10))),
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(4, 15))),
            random.Next(1, 853),
            random.Next(2000, 5000).ToString()
            ),

        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlace("Changsha",DateTime.Now.AddHours(random.Next(4, 18))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),

        new (
            Guid.NewGuid(),
            "Air China",
            new TimePlace("Beijing",DateTime.Now.AddHours(random.Next(1, 21))),
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(4, 21))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),

        new (
            Guid.NewGuid(),
             "China Eastern",
            new TimePlace("Shanghai",DateTime.Now.AddHours(random.Next(1, 23))),
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(4, 25))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),


        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Changsha",DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlace("Shanghai",DateTime.Now.AddHours(random.Next(4, 19))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),


        new (
            Guid.NewGuid(),
            "Hainan Airlines",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 55))),
            new TimePlace("Budapest",DateTime.Now.AddHours(random.Next(4, 58))),
            random.Next(1, 853),
            random.Next(2000, 4000).ToString()
            ),


        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 58))),
            new TimePlace("Munich",DateTime.Now.AddHours(random.Next(4, 60))),
            random.Next(1, 853),
            random.Next(2000, 4000).ToString()
            )
        };

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRmList = flights.Select(flight =>
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
            flight.price
            )).ToArray();

            return flightRmList;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);
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
                flight.price
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
            var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);
            if (flight == null)
                return NotFound();

            var error = flight.AddBooking(dto.Email, dto.NumberOfSeats);
            if (error is OverbookError)
                return Conflict(new { message = "Not enough remaining seats." });

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId }, dto);

        }



    }
}