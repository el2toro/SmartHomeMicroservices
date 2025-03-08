namespace DeviceManagement.API.Devices.GetDevices;

//public record GetDevicesRequest();
public record GetDevicesResponse(IEnumerable<object> Devices);
public class GetDevicesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/devices", async (ISender sender) =>
        {
            var result = await sender.Send(new GetDevicesQuery());
            var response = result.Adapt<GetDevicesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetDevices")
        .Produces<GetDevicesResponse>(StatusCodes.Status200OK)
        .WithDescription("Get Devices")
        .WithSummary("Get Devices");
    }
}
