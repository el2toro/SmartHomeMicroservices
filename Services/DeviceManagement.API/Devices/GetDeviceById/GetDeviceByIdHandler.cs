namespace DeviceManagement.API.Devices.GetDeviceById;

public record GetDeviceByIdQuery(string Id) : ICommand<GetDeviceByIdResult>;
public record GetDeviceByIdResult(string Device);
public class GetDeviceByIdHandler : ICommandHandler<GetDeviceByIdQuery, GetDeviceByIdResult>
{
    public async Task<GetDeviceByIdResult> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        return new GetDeviceByIdResult("device");
    }
}
