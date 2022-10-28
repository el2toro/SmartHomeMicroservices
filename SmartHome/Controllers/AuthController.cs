using Microsoft.AspNetCore.Mvc;
using SmartHome.DTOs;
using SmartHome.Interfaces;
using SmartHome.Models.Auth;
using SmartHome.Models.Common;
using SmartHome.Services;

namespace SmartHome.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;
        public AuthController(IAuthRepository authRepository, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            var result = _authRepository.Authenticate(user);

            if (result == null) return NotFound();

            user.Role = "Admin";
            var responese = _jwtService.GenerateJwtToken(user);

            return Ok(new Response<AuthResponse>(responese));
        }
    }
}
