using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SmartHome.Controllers;
using SmartHome.DTOs;
using SmartHome.Interfaces;
using SmartHome.Models.Auth;
using SmartHome.Models.Common;
using SmartHome.Services;
using System.Net;

namespace SmartHome.Tests.Controllers
{
    public class AuthControllerTests
    {
        private Mock<IAuthRepository> _authRepository;
        private Mock<IJwtService> _jwtService;
        private AuthController _sut;
      
        [SetUp]
        public void SetUp()
        {
            _authRepository = new Mock<IAuthRepository>();
            _jwtService = new Mock<IJwtService>();
            _sut = new AuthController(_authRepository.Object, _jwtService.Object);
        }   

        [Test]  
        public void Login_Should_Return_OK()
        {
            var user = new UserDto()
            {
                Password = "password123",
                Username = "myusername",
                Role = "Admin"
            };

            var response = new AuthResponse()
            {
                Username = user.Username,
                Role = user.Role,
                Expiration = System.DateTime.Now.AddDays(1),
                Token = "my sasihcbsdav8c745sadvasdvaf7v8sd"
            };

            _authRepository.Setup(c => c.Authenticate(It.IsAny<UserDto>())).Returns(user);
            _jwtService.Setup(c => c.GenerateJwtToken(It.IsAny<UserDto>())).Returns(response);

            var result = _sut.Login(user);

            Assert.That(((OkObjectResult)result).StatusCode == StatusCodes.Status200OK);
        }

        [Test]
        public void Login_Should_Return_NotFound()
        {
            var user = new UserDto()
            {
                Password = "password123",
                Username = "myusername",
                Role = "Admin"
            };

            var response = new AuthResponse()
            {
                Username = user.Username,
                Role = user.Role,
                Expiration = System.DateTime.Now.AddDays(1),
                Token = "my sasihcbsdav8c745sadvasdvaf7v8sd"
            };

            _authRepository.Setup(c => c.Authenticate(It.IsAny<UserDto>())).Returns(user = null);
            _jwtService.Setup(c => c.GenerateJwtToken(It.IsAny<UserDto>())).Returns(response);

            var result = _sut.Login(user);

            Assert.That(((NotFoundResult)result).StatusCode == StatusCodes.Status404NotFound);
        }
    }
}
