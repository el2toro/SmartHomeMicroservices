namespace DeviceManagement.API.Devices.GetDevices;

public record GetDevicesQuery() : IQuery<GetDevicesResult>;
public record GetDevicesResult(IEnumerable<object> Devices);
internal class GetDevicesHandler(MongoDbContext dbContext)
    : IQueryHandler<GetDevicesQuery, GetDevicesResult>
{
    public async Task<GetDevicesResult> Handle(GetDevicesQuery query, CancellationToken cancellationToken)
    {
        // TO use repository pattern??
        var result = await dbContext.DeviceCollection.FindAsync(c => true);

        return new GetDevicesResult(AdaptResult(result));
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
