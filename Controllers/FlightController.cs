using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using midterm_project.Model;
using Repos;

namespace Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]

    public class FlightController : ControllerBase
    {

        private IConfiguration _configuration;
        public FlightController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetFlights([FromQuery] QueryTicket model)
        {
            var fligthRepo = new FlightRepo();
            var flight = fligthRepo.GetFlights(model);
            return Ok(flight);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateFlight([FromBody] Flight model)
        {
            var fligthRepo = new FlightRepo();
            var flight = fligthRepo.CreateFlight(model);
            return Ok(flight);
        }

        [HttpPost, Authorize]
        public IActionResult BuyTicket([FromBody] BuyTicket model)
        {
            var fligthRepo = new FlightRepo();
            var clientRepo = new ClientRepo(_configuration);

            var token = TokenManager.GetToken(Request);

            var client = clientRepo.GetClientByToken(token);
            if (client != null)
            {
                var response = fligthRepo.BuyTicket(model, client);
                return Ok(response);
            }
            else
            {
                return Ok("Invalid Client Token !!!");
            }
        }
    }
}