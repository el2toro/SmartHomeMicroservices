using SmartHome.Context;
using SmartHome.DTOs;
using SmartHome.Interfaces;

namespace SmartHome.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SmartHomeContext _context;
        public AuthRepository(SmartHomeContext context)
        {
            _context = context;
        }
        public UserDto Authenticate(UserDto user)
        {
            var result = _context.Users.Where(u => u.Username == user.Username && u.Password == user.Password)
                                       .FirstOrDefault();

            if (result != null)
            {
                return new UserDto
                {
                    Username = result.Username,
                    Password = result.Password,
                    Role = result.Role,
                };
            }

            return null;
        }

    }
}
