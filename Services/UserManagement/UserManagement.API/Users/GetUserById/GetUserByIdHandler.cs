using Core.CQRS;
using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.Models;

namespace UserManagement.API.Users.GetUserById;

public record GetUserByIdQuery(int Id) : IQuery<GetUserByIdResult>;
public record GetUserByIdResult(User User);
internal class GetUserByIdQueryHandler(UserDbContext dbContext)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        User user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == query.Id, cancellationToken);

        if (user == null)
        {
            throw new ArgumentNullException();
        }
        return new GetUserByIdResult(user);
    }
}
