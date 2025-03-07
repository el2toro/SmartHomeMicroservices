namespace DeviceManagement.API.Devices.CreateDevice;

public record CreateDeviceRequest(string Device);
public record CreateDeviceResponse(bool IsSuccess);
public class CreateDeviceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/device", async ([FromBody] JsonElement device, ISender sender) =>
        {
            var result = await sender.Send(new CreateDeviceCommand(device));
            //var response = result.Adapt<CreateDeviceResponse>();
            return Results.Created();
        })
        .WithName("CreateDevice")
        .Produces<CreateDeviceResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Device")
        .WithDescription("Create Device");
    }
}
