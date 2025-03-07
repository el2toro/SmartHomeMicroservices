using Core.Exceptions;

namespace UserManagement.API.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(int id) : base("Product", id)
    {
    }
}
