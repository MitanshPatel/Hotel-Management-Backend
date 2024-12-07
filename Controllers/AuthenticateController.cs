using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Models;
using Hostel_Management.Services;
using Hostel_Management.Services.JWT;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            // Only guests can register themselves
            if (user.Role != "Guest")
            {
                return BadRequest(new { msg = "Only guests can register themselves." });
            }

            if (!_userService.RegisterUser(user))
            {
                return BadRequest(new { msg = "User already exists" });
            }

            return Ok(new { msg = "User registered successfully" });
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("register-admin-manager")]
        public IActionResult RegisterAdminManager([FromBody] User user)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole == "Admin")
            {
                // Admin can register any role except Guest
                if (user.Role == "Guest")
                {
                    return BadRequest(new { msg = "Admin cannot register a Guest." });
                }
            }
            else if (currentUserRole == "Manager")
            {
                // Manager can register any role except Guest and Admin
                if (user.Role == "Guest" || user.Role == "Admin")
                {
                    return BadRequest(new { msg = "Manager cannot register a Guest or Admin." });
                }
            }
            else
            {
                return Forbid();
            }

            if (!_userService.RegisterUser(user))
            {
                return BadRequest(new { msg = "User already exists" });
            }

            return Ok(new { msg = "User registered successfully" });
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


