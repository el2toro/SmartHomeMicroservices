using DeviceManagement.API.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeviceManagement.API.Devices.DeleteDevice;

public record DeleteDeviceCommand(int Id) : ICommand<DeleteDeviceResult>;
public record DeleteDeviceResult(bool IsSuccess);
internal class DeleteDeviceHandler(IMongoDbConfiguration mongoDbConfiguration)
    : ICommandHandler<DeleteDeviceCommand, DeleteDeviceResult>
{
    public async Task<DeleteDeviceResult> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", command.Id);

        await collection.DeleteOneAsync(filter);

        return new DeleteDeviceResult(true);
    }
}
