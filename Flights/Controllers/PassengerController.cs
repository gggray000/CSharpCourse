using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;
using Flights.Domain.Entities;
using Flights.ReadModels;
using Flights.Data;

namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _entities.Passengers.Add(
                new Passenger(
                    dto.email,
                    dto.firstName,
                    dto.lastName,
                    dto.gender
                )
            );
            System.Diagnostics.Debug.WriteLine(_entities.Passengers.Count);
            return CreatedAtAction(nameof(Find), new {email = dto.email}, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(p => p.email == email);

            if (passenger == null)
                return NotFound();

            var passengerRm = new PassengerRm(
                passenger.email,
                passenger.firstName,
                passenger.lastName,
                passenger.gender
            );

            return Ok(passengerRm);
        }
    }
}