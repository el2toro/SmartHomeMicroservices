using DeviceManagement.API.Devices.EvenHandlers;

namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement DeviceAsJson) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);

public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceCommandValidator()
    {
        //TODO: Add more rules
        RuleFor(x => x.DeviceAsJson).NotNull().WithMessage("Device object is required");
    }
}

internal class CreateDeviceHandler(IDeviceRepository deviceRepository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateDeviceCommand, CreateDeviceResult>
{
    public async Task<CreateDeviceResult> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        Guid deviceId = command.DeviceAsJson.GetProperty(DeviceConstants.DEVICE_ID).GetGuid();

        var result = await deviceRepository.GetDeviceById(deviceId);

        if (result is not null)
        {
            throw new BadRequestException($"Device with id: {deviceId} already exists in database");
        }

        await deviceRepository.CreateDevice(command.DeviceAsJson, cancellationToken);

        await publishEndpoint.Publish(new DeviceCreatedEvent(command.DeviceAsJson), cancellationToken);

        return new CreateDeviceResult(true);
    }
}
