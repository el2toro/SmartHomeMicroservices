namespace UserManagement.API.Users.CreateUser;

public record CreateUserRequest(UserDto UserDto);
public record CreateUserResponse(bool IsSuccess);
public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/user", async (UserDto usedDto, ISender sender) =>
        {
            var result = await sender.Send(new CreateUserCommand(usedDto));
            var response = result.Adapt<CreateUserResponse>();

            return Results.Created();
        })
        .WithName("CreateUser")
        .Produces<CreateUserResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create User")
        .WithDescription("Create User");
    }
}