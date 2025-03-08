namespace DeviceManagement.API.Devices.CreateDevice;

public record CreateDeviceRequest(JsonElement DeviceAsJson);
public record CreateDeviceResponse(bool IsSuccess);
public class CreateDeviceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/device", async ([AsParameters] CreateDeviceRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateDeviceCommand>();
            var result = await sender.Send(command);

            return Results.Created();
        })
        .WithName("CreateDevice")
        .Produces<CreateDeviceResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Device")
        .WithDescription("Create Device");
    }
}
