using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SOA.SecurityApi.Models;
using SOA.SecurityApi.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace SOA.SecurityApi.Controllers
{
    [Authorize]
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
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("validation")]
        public IActionResult Validation()
        {
            var username = HttpContext.User.Identity.Name;
           return Ok(username);
        }
    }
}
