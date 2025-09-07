using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;
using Flights.Domain.Entities;
using Flights.ReadModels;
using Flights.Data;
using Microsoft.AspNetCore.Identity;

namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;
        private readonly IPasswordHasher<Passenger> _hasher;

        public PassengerController(Entities entities, IPasswordHasher<Passenger> hasher)
        {
            _entities = entities;
            _hasher = hasher;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            if (_entities.Passengers.Any(p => p.Email == dto.email))
                return Conflict($"Passenger with email '{dto.email}' already exists.");

            Passenger passenger = new Passenger
            (
                dto.email,
                "",
                dto.firstName,
                dto.lastName,
                dto.gender
            );

            passenger.HashedPassword = _hasher.HashPassword(passenger, dto.password);

            _entities.Passengers.Add(passenger);

            _entities.SaveChanges();
            return CreatedAtAction(nameof(Find), new { email = dto.email }, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);

            if (passenger == null)
                return NotFound();

            var passengerRm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
            );

            return Ok(passengerRm);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public ActionResult<PassengerRm> Login(LoginDto dto)
        {
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == dto.email);

            if (passenger == null)
                return Unauthorized("Invalid email or password.");

            if (_hasher.VerifyHashedPassword(passenger, passenger.HashedPassword, dto.password)
                    == PasswordVerificationResult.Failed
                )
                return Unauthorized("Invalid email or password.");

            return Ok(new { Message = "Login Successful" });
        }
    }
}