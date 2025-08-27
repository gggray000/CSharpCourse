using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;
using Flights.Domain.Entities;
using Flights.ReadModels;

namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        private static IList<Passenger> Passengers = new List<Passenger>();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            Passengers.Add(
                new Passenger(
                    dto.email,
                    dto.firstName,
                    dto.lastName,
                    dto.gender
                )
            );
            System.Diagnostics.Debug.WriteLine(Passengers.Count);
            return CreatedAtAction(nameof(Find), new {email = dto.email}, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = Passengers.FirstOrDefault(p => p.email == email);

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