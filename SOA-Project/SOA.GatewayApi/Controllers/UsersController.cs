using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SOA.GatewayApi.Models;
using SOA.GatewayApi.Services;
using System.Threading.Tasks;

namespace SOA.GatewayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
