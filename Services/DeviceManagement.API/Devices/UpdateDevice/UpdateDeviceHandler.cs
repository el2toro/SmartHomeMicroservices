using DeviceManagement.API.Models;

namespace DeviceManagement.API.Devices.UpdateDevice;

public record UpdateDeviceCommand(JsonElement DeviceAsJson) : ICommand<UpdateDeviceResult>;
public record UpdateDeviceResult(bool IsSuccess);
internal class UpdateDeviceHandler(IMongoDbConfiguration mongoDbConfiguration)
    : ICommandHandler<UpdateDeviceCommand, UpdateDeviceResult>
{
    public async Task<UpdateDeviceResult> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        int deviceId = command.DeviceAsJson.GetProperty("deviceId").GetInt32();
        int deviceType = command.DeviceAsJson.GetProperty("deviceType").GetInt32();

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", deviceId);
        var json = BsonDocument.Parse(command.DeviceAsJson.GetRawText());

        var update = GetUpdateDefinitionForLightDevice(command.DeviceAsJson);

        //Return updated result?
        var data = await collection.UpdateOneAsync(filter, update);


        return new UpdateDeviceResult(true);
    }

    private UpdateDefinition<BsonDocument> GetUpdateDefinitionForLightDevice(JsonElement deviceAsJson)
    {
        //var device = JsonSerializer.Deserialize<LightDevice>(deviceAsJson);

        var update = Builders<BsonDocument>.Update.Set("color", "yellow")
             .Set("brightness", "light light");

        return update;
    }
}
