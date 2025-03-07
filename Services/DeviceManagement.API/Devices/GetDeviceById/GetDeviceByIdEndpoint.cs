namespace DeviceManagement.API.Devices.GetDeviceById;

//public record GetDeviceByIdRequest(string Id);
public record GetDeviceByIdResponse(string Device);
public class GetDeviceByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/device/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new GetDeviceByIdQuery(id));
            var response = result.Adapt<GetDeviceByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetDeviceById")
        .Produces<GetDeviceByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Device By Id")
        .WithDescription("Get Device By Id");
    }
}
