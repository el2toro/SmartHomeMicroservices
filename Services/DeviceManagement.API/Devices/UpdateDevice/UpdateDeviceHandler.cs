namespace DeviceManagement.API.Devices.UpdateDevice;

public record UpdateDeviceCommand(JsonElement DeviceAsJson) : ICommand<UpdateDeviceResult>;
public record UpdateDeviceResult(object Device);

public class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator()
    {
        //TODO: add more rules
        RuleFor(x => x.DeviceAsJson).NotNull().WithMessage("Device Object is required");
    }
}

internal class UpdateDeviceHandler(IDeviceRepository deviceRepository)
    : ICommandHandler<UpdateDeviceCommand, UpdateDeviceResult>
{
    public async Task<UpdateDeviceResult> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        var result = await deviceRepository.UpdateDevice(command.DeviceAsJson, cancellationToken);

        return new UpdateDeviceResult(result.ToDevice());
    }
}
