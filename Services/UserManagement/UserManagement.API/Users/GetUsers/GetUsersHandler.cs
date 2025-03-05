using UserManagement.API.Models;
using Core.CQRS;
using UserManagement.API.Data;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.API.Users.GetUsers;

public record GetUsersQuery() : IQuery<GetUsersResult>;
public record GetUsersResult(IEnumerable<User> Users);
internal class GetUsersQueryHandler(UserDbContext dbContext)
    : IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.ToListAsync(cancellationToken);

        return new GetUsersResult(users);
    }
}
