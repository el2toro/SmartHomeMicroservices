using DeviceManagement.API.Configuration;

namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement Device) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);

internal class CreateDeviceHandler(IMongoDbConfiguration mongoDbConfiguration)
    : ICommandHandler<CreateDeviceCommand, CreateDeviceResult>
{
    public async Task<CreateDeviceResult> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        await collection.InsertOneAsync(BsonDocument.Parse(command.Device.GetRawText()));

        return new CreateDeviceResult(true);
    }
}
