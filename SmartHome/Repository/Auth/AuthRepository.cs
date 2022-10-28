using AutoMapper;
using SmartHome.Context;
using SmartHome.DTOs;
using SmartHome.Interfaces;

namespace SmartHome.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SmartHomeContext _context;
        private readonly IMapper _mapper;
        public AuthRepository(SmartHomeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserDto Authenticate(UserDto user)
        {
            var result = _context.Users.Where(u => u.Username == user.Username && u.Password == user.Password)
                                       .FirstOrDefault();

            if (result != null)
            {
                return _mapper.Map<UserDto>(result);
            }

            return null;
        }

    }
}
