namespace UserManagement.API.Users.UpdateUser;

public record UpdateUserCommand(UserDto UserDto, int UserId) : ICommand<UpdateUserResult>;
public record UpdateUserResult(bool IsSuccess);

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.UserDto.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.UserDto.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.UserDto.Password).NotEmpty().WithMessage("Password is required");
    }
}

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
