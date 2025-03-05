namespace UserManagement.API.Users.UpdateUser;

public record UpdateUserCommand(UserDto UserDto, int UserId) : ICommand<UpdateUserResult>;
public record UpdateUserResult(bool IsSuccess);
internal class UpdateUserHandler(UserDbContext dbContext)
    : ICommandHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        User user = await dbContext.Users.FindAsync(command.UserId) ??
            throw new UserNotFoundException(command.UserId);

        user.UserName = command.UserDto.UserName;
        user.Password = command.UserDto.Password;
        user.Email = command.UserDto.Email;

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateUserResult(true);
    }
}
