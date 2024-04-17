using ContactPageApi.Models.Dto;
using ContactPageApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactPageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                if (await _authService.RegisterUser(user))
                {
                    return Ok("Registration successful");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to register user");
                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid details");
            }

            if (await _authService.LoginUser(user))
            {
                var tokenString = await _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }

            return BadRequest("Login failed");
        }

    }
}
