using Carter;
using MediatR;

namespace DeviceManagement.API.Devices.CreateDevice;

public record CreateDeviceRequest(string Device);
public record CreateDeviceResponse(bool IsSuccess);
public class CreateDeviceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/device", async (string device, ISender sender) =>
        {
            var response = await sender.Send(device);
            return Results.Created();
        });
    }
}
