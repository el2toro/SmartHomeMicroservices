namespace UserManagement.API.Users.GetUserById;

public record GetUserByIdQuery(int Id) : IQuery<GetUserByIdResult>;
public record GetUserByIdResult(User User);
internal class GetUserByIdQueryHandler(UserDbContext dbContext)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        User user = await dbContext.Users.FindAsync(query.Id, cancellationToken) ??
            throw new ArgumentNullException();

        return new GetUserByIdResult(user);
    }
}
