using Carter;
using Mapster;
using MediatR;
using UserManagement.API.Models;

namespace UserManagement.API.Users.GetUsers;

public record GetUsersRequest();
public record GetUsersResponse(IEnumerable<User> Users);
public class GetUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (ISender sender) =>
        {
            //var query = sender.Adapt<GetUsersQuery>();
            var result = await sender.Send(new GetUsersQuery());
            var response = result.Adapt<GetUsersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetUsers")
        .Produces<GetUsersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Users")
        .WithDescription("Get Users");
    }
}
