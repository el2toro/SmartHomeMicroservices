using DeviceManagement.API.Constants;

namespace DeviceManagement.API.Devices.GetDeviceById;

public record GetDeviceByIdQuery(Guid Id) : ICommand<GetDeviceByIdResult>;
public record GetDeviceByIdResult(object Device);
public class GetDeviceByIdHandler(MongoDbContext dbContext)
    : ICommandHandler<GetDeviceByIdQuery, GetDeviceByIdResult>
{
    public async Task<GetDeviceByIdResult> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        //TODO: 
        //Add fluent validation
        var filter = Builders<BsonDocument>.Filter.Eq(DeviceConstants.DEVICE_ID, query.Id.ToString());

        var result = await dbContext.DeviceCollection.Find(filter).FirstOrDefaultAsync() ??
             throw new DeviceNotFoundException(query.Id.ToString());

        return new GetDeviceByIdResult(result.ToDevice());
    }
}
