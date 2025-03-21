namespace DeviceManagement.API.Devices.DeleteDevice;

public record DeleteDeviceCommand(Guid Id) : ICommand<DeleteDeviceResult>;
public record DeleteDeviceResult(bool IsSuccess);
internal class DeleteDeviceHandler(IDeviceRepository deviceRepository)
    : ICommandHandler<DeleteDeviceCommand, DeleteDeviceResult>
{
    public async Task<DeleteDeviceResult> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        //TODO: 
        //Add fluent validation

        await deviceRepository.DeleteDevice(command.Id);

        return new DeleteDeviceResult(true);
    }
}
