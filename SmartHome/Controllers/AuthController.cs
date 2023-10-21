using Microsoft.AspNetCore.Mvc;
using SmartHome.DTOs;
using SmartHome.Interfaces;
using SmartHome.Models.Auth;
using SmartHome.Models.Common;
using SmartHome.Services;
using System.Net;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UserDto user)
        {
            if (!_authRepository.CheckUserExist(user.Username)) return NotFound();

            var result = _authRepository.Authenticate(user);

            if (result == null) return BadRequest();

            user.Role = "Admin";
            var response = _jwtService.GenerateJwtToken(user);

            return Ok(new Response<AuthResponse>(response));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RessetPassword([FromBody] RessetPassword ressetPassword)
        {
            if (ressetPassword == null) return BadRequest("Not valid parameters.");  

            var result = _authRepository.RessetPassword(ressetPassword);

            if (!result) return NotFound();

            return Ok(new Response<string>("Password changed succesfuly!"));
        }
    }
}
