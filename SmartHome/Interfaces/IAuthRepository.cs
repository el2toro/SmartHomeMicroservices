using SmartHome.DTOs;
using SmartHome.Models.Auth;

namespace SmartHome.Interfaces
{
    public interface IAuthRepository
    {
        UserDto Authenticate(UserDto user);
        bool ChangePassword(ChangePassword changePassword);
    }
}
