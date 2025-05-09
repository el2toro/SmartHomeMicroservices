﻿namespace UserManagement.API.Users.GetUserById;

//public record GetUserByIdRequest(int Id);
public record GetUserByIdResponse(User User);
public class GetUserByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/user/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(id));
            var response = result.Adapt<GetUserByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetUserById")
        .Produces<GetUserByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get User By Id")
        .WithDescription("Get User By Id");
    }
}