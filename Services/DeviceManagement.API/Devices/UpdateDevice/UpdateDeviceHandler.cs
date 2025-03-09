namespace DeviceManagement.API.Devices.UpdateDevice;

public record UpdateDeviceCommand(JsonElement DeviceAsJson) : ICommand<UpdateDeviceResult>;
public record UpdateDeviceResult(object Device);
internal class UpdateDeviceHandler(MongoDbContext dbContext, IDeviceData deviceData)
    : ICommandHandler<UpdateDeviceCommand, UpdateDeviceResult>
{
    public async Task<UpdateDeviceResult> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        Guid deviceId = command.DeviceAsJson.GetProperty("deviceId").GetGuid();

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", deviceId);

        var updateDefinition = deviceData.GetUpdateDeviceDefinition(command.DeviceAsJson);

        var result = await dbContext.DeviceCollection.FindOneAndUpdateAsync(filter, updateDefinition);

        return new UpdateDeviceResult(result.ToDevice());
    }
}
