namespace UserManagement.API.Users.DeleteUser;

//public record DeleteUserRequest(int Id);
public record DeleteUserResponse(User User);
public class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/user/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteUserCommand(id));
            var response = result.Adapt<DeleteUserResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteUser")
        .Produces<DeleteUserResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete User")
        .WithDescription("Delete User");
    }
}