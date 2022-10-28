using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHome.DTOs;
using SmartHome.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartHome.Services
{
    public interface IJwtService
    {
        AuthResponse GenerateJwtToken(UserDto user);
    }
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;
      
        public AuthResponse GenerateJwtToken(UserDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));

            double tokenExpireTime = Convert.ToDouble(_appSettings.ExpireTime);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("LogedOn", DateTime.Now.ToString())
                }),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Site,
                Audience = _appSettings.Audience,
                Expires = DateTime.Now.AddMinutes(tokenExpireTime),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo,
                Username = user.Username,
                Role = user.Role,
            };
        }
    }
}
