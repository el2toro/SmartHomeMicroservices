using Microsoft.AspNetCore.Mvc;
using SmartHome.DTOs;
using SmartHome.Interfaces;
using SmartHome.Models.Auth;
using SmartHome.Models.Common;
using SmartHome.Services;

namespace SmartHome.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJWTService _jwtService;
        public AuthController(IAuthRepository authRepository, IJWTService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            var result = _authRepository.Authenticate(user);

            if (result == null) return NotFound();

            user.Role = "Admin";
            var responese = _jwtService.GenerateJWTToken(user);

            return Ok(new Response<AuthResponse>(responese));
        }
    }
}
