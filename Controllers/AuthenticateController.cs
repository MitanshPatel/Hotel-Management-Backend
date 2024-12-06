using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Models;
using Hostel_Management.Services;
using Hostel_Management.Services.JWT;
using Microsoft.AspNetCore.Http;

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
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.FindFirst("role")?.Value;

            if (currentUserRole == null)
            {
                // Only guests can register themselves
                if (user.Role != "Guest")
                {
                    return Forbid("Only guests can register themselves.");
                }
            }
            else if (currentUserRole == "Admin")
            {
                // Admin can register new admins, managers, receptionists, and housekeeping staff
                if (user.Role != "Admin" && user.Role != "Manager" && user.Role != "Receptionist" && user.Role != "Housekeeping")
                {
                    return Forbid("Admin can only register new admins, managers, receptionists, and housekeeping staff.");
                }
            }
            else if (currentUserRole == "Manager")
            {
                // Manager can register new receptionists and housekeeping staff
                if (user.Role != "Receptionist" && user.Role != "Housekeeping")
                {
                    return Forbid("Manager can only register new receptionists and housekeeping staff.");
                }
            }
            else
            {
                return Forbid("You do not have permission to register new users.");
            }

            if (!_userService.RegisterUser(user))
            {
                return BadRequest("User already exists");
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
