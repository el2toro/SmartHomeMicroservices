using DeviceManagement.API.Configuration;

namespace DeviceManagement.API.Devices.GetDeviceById;

public record GetDeviceByIdQuery(int Id) : ICommand<GetDeviceByIdResult>;
public record GetDeviceByIdResult(object Device);
public class GetDeviceByIdHandler(IMongoDbConfiguration mongoDbConfiguration)
    : ICommandHandler<GetDeviceByIdQuery, GetDeviceByIdResult>
{
    public async Task<GetDeviceByIdResult> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", query.Id);
        var result = await collection.Find(filter).FirstOrDefaultAsync();

        return new GetDeviceByIdResult(result.ToDevice());
    }
}
