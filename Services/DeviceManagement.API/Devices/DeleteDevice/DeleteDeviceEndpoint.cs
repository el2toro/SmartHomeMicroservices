namespace DeviceManagement.API.Devices.DeleteDevice;

//public record DeleteDeviceRequest();
public record DeleteDeviceResponse(bool IsSuccess);
public class DeleteDeviceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/device/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteDeviceCommand(Guid.Parse(id)));
            var response = result.Adapt<DeleteDeviceResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteDevice")
        .Produces<DeleteDeviceResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Device")
        .WithDescription("Delete Device");
    }
}
