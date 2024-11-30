using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Models;
using Hostel_Management.Services;
using Hostel_Management.Services.JWT;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserService _userService;

        public AuthenticateController(JwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (!_userService.RegisterUser(user))
            {
                return BadRequest("User already exists");
            }

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.AuthenticateUser(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtTokenService.GenerateToken(user.UserId, user.Role);
            return Ok(new { Token = token });
        }
    }
}
