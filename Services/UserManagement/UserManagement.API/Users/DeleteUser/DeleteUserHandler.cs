namespace UserManagement.API.Users.DeleteUser;

public record DeleteUserCommand(int Id) : ICommand<DeleteUserResult>;
public record DeleteUserResult(bool IsSuccess);
internal class DeleteUserHandler(UserDbContext dbContext)
    : ICommandHandler<DeleteUserCommand, DeleteUserResult>
{
    public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            User user = await dbContext.Users.FindAsync(command.Id, cancellationToken) ??
                throw new ArgumentNullException();


            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteUserResult(true);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
