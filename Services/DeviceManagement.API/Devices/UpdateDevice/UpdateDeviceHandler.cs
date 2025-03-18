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

internal class UpdateDeviceHandler(MongoDbContext dbContext, IDeviceData deviceData)
    : ICommandHandler<UpdateDeviceCommand, UpdateDeviceResult>
{
    public async Task<UpdateDeviceResult> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        Guid deviceId = command.DeviceAsJson.GetProperty(DeviceConstants.DEVICE_ID).GetGuid();

        var filter = Builders<BsonDocument>.Filter.Eq(DeviceConstants.DEVICE_ID, deviceId);

        var updateDefinition = deviceData.GetUpdateDeviceDefinition(command.DeviceAsJson);

        var result = await dbContext.DeviceCollection.FindOneAndUpdateAsync(filter, updateDefinition);

        return new UpdateDeviceResult(result.ToDevice());
    }
}
