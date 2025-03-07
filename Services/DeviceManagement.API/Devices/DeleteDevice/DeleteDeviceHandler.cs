namespace DeviceManagement.API.Devices.DeleteDevice;

public record DeleteDeviceCommand(string Id) : ICommand<DeleteDeviceResult>;
public record DeleteDeviceResult(bool IsSuccess);
internal class DeleteDeviceHandler : ICommandHandler<DeleteDeviceCommand, DeleteDeviceResult>
{
    public async Task<DeleteDeviceResult> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        return new DeleteDeviceResult(true);
    }
}
