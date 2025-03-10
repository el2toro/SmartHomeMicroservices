namespace DeviceManagement.API.Devices.UpdateDevice;

public record UpdateDeviceRequest(JsonElement DeviceAsJson);
public record UpdateDeviceResponse(object Device);
public class UpdateDeviceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("device", async ([AsParameters] UpdateDeviceRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateDeviceCommand>();
            var result = await sender.Send(command);

            return Results.Ok(request);
        })
        .WithName("UpdateDevice")
        .Produces<UpdateDeviceResponse>(StatusCodes.Status200OK)
        .Produces<UpdateDeviceResponse>(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Device")
        .WithDescription("Update Device");
    }
}
