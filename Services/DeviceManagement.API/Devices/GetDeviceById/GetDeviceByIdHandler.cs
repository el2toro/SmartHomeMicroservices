namespace DeviceManagement.API.Devices.GetDeviceById;

public record GetDeviceByIdQuery(Guid Id) : ICommand<GetDeviceByIdResult>;
public record GetDeviceByIdResult(object Device);
public class GetDeviceByIdHandler(MongoDbContext dbContext)
    : ICommandHandler<GetDeviceByIdQuery, GetDeviceByIdResult>
{
    public async Task<GetDeviceByIdResult> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", query.Id.ToString());

        var result = await dbContext.DeviceCollection.Find(filter).FirstOrDefaultAsync() ??
             throw new DeviceNotFoundException(query.Id.ToString());

        return new GetDeviceByIdResult(result.ToDevice());
    }
}
