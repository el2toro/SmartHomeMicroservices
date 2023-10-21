using AutoMapper;
using SmartHome.Context;
using SmartHome.DTOs;
using SmartHome.Interfaces;
using SmartHome.Models.Auth;

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

        public bool CheckUserExist(string userName)
        {
            var result = _context.Users.Where(u => u.Username == userName).FirstOrDefault();

            return result != null;
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

        public bool RessetPassword(RessetPassword changePassword)
        {
            int result = 0;

            try
            {
                var user = _context.Users.Where(u => u.Username == changePassword.Username && u.Password == changePassword.OldPassword)
                                       .SingleOrDefault();
                if (user != null)
                {
                    user.Password = changePassword.NewPassword;
                    result = _context.SaveChanges();

                    return (result is not 0) ? true : false;
                }
                
                return false;
                
            }
            catch (Exception)
            {
                throw;
            }          
        }
    }
}
