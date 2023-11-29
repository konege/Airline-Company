using Microsoft.AspNetCore.Mvc;
using Repos;

namespace Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class ClientController : ControllerBase
    {
       private  IConfiguration _configuration;
        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login model)
        {
            var clientRepo = new ClientRepo(_configuration);
            var client = clientRepo.GetClientLogin(model);
            return Ok(client);
        }

        [HttpPost]
        public IActionResult SignUp([FromBody]SignUp model){
            var clientRepo = new ClientRepo(_configuration);
            var client=clientRepo.CreateClient(model);
            return Ok(client);
        }
    }
}