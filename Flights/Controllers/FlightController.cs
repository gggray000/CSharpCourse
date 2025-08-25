using Microsoft.AspNetCore.Mvc;
using Flights.ReadModels;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        static Random random = new Random();

        private static FlightRm[] flights = new FlightRm[]
        {
        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(1, 3))),
            new TimePlaceRm("Vienna",DateTime.Now.AddHours(random.Next(4, 10))),
            random.Next(1, 853),
            random.Next(2000, 5000).ToString()
        ),

        new (
            Guid.NewGuid(),
            "Lufthansa",
            new TimePlaceRm("Berlin",DateTime.Now.AddHours(random.Next(1, 10))),
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(4, 15))),
            random.Next(1, 853),
            random.Next(2000, 5000).ToString()
            ),

        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlaceRm("Changsha",DateTime.Now.AddHours(random.Next(4, 18))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),

        new (
            Guid.NewGuid(),
            "Air China",
            new TimePlaceRm("Beijing",DateTime.Now.AddHours(random.Next(1, 21))),
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(4, 21))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),

        new (
            Guid.NewGuid(),
             "China Eastern",
            new TimePlaceRm("Shanghai",DateTime.Now.AddHours(random.Next(1, 23))),
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(4, 25))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),


        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlaceRm("Changsha",DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlaceRm("Shanghai",DateTime.Now.AddHours(random.Next(4, 19))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),


        new (
            Guid.NewGuid(),
            "Hainan Airlines",
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(1, 55))),
            new TimePlaceRm("Budapest",DateTime.Now.AddHours(random.Next(4, 58))),
            random.Next(1, 853),
            random.Next(2000, 4000).ToString()
            ),


        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlaceRm("Guangzhou",DateTime.Now.AddHours(random.Next(1, 58))),
            new TimePlaceRm("Munich",DateTime.Now.AddHours(random.Next(4, 60))),
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
        => flights;

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
            return Ok(flight);
        }


    }
}