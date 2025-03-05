using UserManagement.API.Users.UpdateUser;

namespace UserManagement.API.Users.GetUsers;

public record UpdateUserRequest(UserDto UserDto, int UserId);
public record UpdateUserResponse(bool IsSuccess);
public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/user/{id}", async (UserDto usedDto, int id, ISender sender) =>
        {
            var result = await sender.Send(new UpdateUserCommand(usedDto, id));
            var response = result.Adapt<UpdateUserResponse>();

            return Results.Created();
        })
        .WithName("UpdateUser")
        .Produces<UpdateUserResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update User")
        .WithDescription("Update User");
    }
}