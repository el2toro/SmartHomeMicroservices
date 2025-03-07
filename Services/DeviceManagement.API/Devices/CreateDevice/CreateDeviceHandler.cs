using DeviceManagement.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement Device) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);
internal class CreateDeviceHandler : ICommandHandler<CreateDeviceCommand, CreateDeviceResult>
{
    public async Task<CreateDeviceResult> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("devicedb");
        var collection = database.GetCollection<BsonDocument>("devicecollection");

        // Insert a document
        await collection.InsertOneAsync(BsonDocument.Parse(command.Device.GetRawText()));

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", "id1");
        var result = await collection.Find(filter).ToListAsync();

        return new CreateDeviceResult(true);
    }
}
