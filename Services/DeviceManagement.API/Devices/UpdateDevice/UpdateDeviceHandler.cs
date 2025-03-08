using DeviceManagement.API.Configuration;
using DeviceManagement.API.Data;

namespace DeviceManagement.API.Devices.UpdateDevice;

public record UpdateDeviceCommand(JsonElement DeviceAsJson) : ICommand<UpdateDeviceResult>;
public record UpdateDeviceResult(object Device);
internal class UpdateDeviceHandler(IMongoDbConfiguration mongoDbConfiguration, IDeviceData deviceData)
    : ICommandHandler<UpdateDeviceCommand, UpdateDeviceResult>
{
    public async Task<UpdateDeviceResult> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        int deviceId = command.DeviceAsJson.GetProperty("deviceId").GetInt32();

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", deviceId);

        var updateDefinition = deviceData.GetUpdateDeviceDefinition(command.DeviceAsJson);

        var result = await collection.FindOneAndUpdateAsync(filter, updateDefinition);

        return new UpdateDeviceResult(result.ToDevice());
    }
}
