using MongoDB.Bson;
using System.Reflection.Metadata;

namespace DeviceManagement.API.Devices.GetDevices;

public record GetDevicesQuery() : IQuery<GetDevicesResult>;
public record GetDevicesResult(IEnumerable<object> Devices);
internal class GetDevicesHandler(IMongoDbConfiguration mongoDbConfiguration)
    : IQueryHandler<GetDevicesQuery, GetDevicesResult>
{
    public async Task<GetDevicesResult> Handle(GetDevicesQuery query, CancellationToken cancellationToken)
    {
        var collection = mongoDbConfiguration.GetCollection();

        var result = await collection.FindAsync(c => true);

        var data = AdaptResult(result);

        return new GetDevicesResult(data);
    }

    private IEnumerable<object> AdaptResult(IAsyncCursor<BsonDocument> document)
    {
        List<object> result = new List<object>();

        document.ForEachAsync(c =>
        {
            result.Add(c.ToDevice());

        });

        return result;
    }
}
