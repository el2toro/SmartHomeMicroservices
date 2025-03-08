using DeviceManagement.API.Extensions;
using MongoDB.Bson;

namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement Device) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);
internal class CreateDeviceHandler(IMongoDbConfiguration mongoDbConfiguration)
    : ICommandHandler<CreateDeviceCommand, CreateDeviceResult>
{
    public async Task<CreateDeviceResult> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        // Insert a document
        await collection.InsertOneAsync(BsonDocument.Parse(command.Device.GetRawText()));

        return new CreateDeviceResult(true);
    }
}
