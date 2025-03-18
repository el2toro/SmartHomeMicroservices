namespace UserManagement.API.Users.CreateUser;

public record CreateUserCommand(UserDto UserDto) : ICommand<CreateUserResult>;
public record CreateUserResult(bool IsSuccess);

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserDto.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.UserDto.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.UserDto.Password).NotEmpty().WithMessage("Password is required");
    }
}
internal class CreateUserHandler(UserDbContext dbContext)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        User user = new()
        {
            UserName = command.UserDto.UserName,
            Email = command.UserDto.Email,
            Password = command.UserDto.Password
        };

        try
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateUserResult(true);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
