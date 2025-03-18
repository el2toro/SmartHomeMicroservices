using DeviceManagement.API.Constants;

namespace DeviceManagement.API.Devices.DeleteDevice;

public record DeleteDeviceCommand(Guid Id) : ICommand<DeleteDeviceResult>;
public record DeleteDeviceResult(bool IsSuccess);
internal class DeleteDeviceHandler(MongoDbContext dbContext)
    : ICommandHandler<DeleteDeviceCommand, DeleteDeviceResult>
{
    public async Task<DeleteDeviceResult> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        //TODO: 
        //Add fluent validation
        var filter = Builders<BsonDocument>.Filter.Eq(DeviceConstants.DEVICE_ID, command.Id);

        await dbContext.DeviceCollection.DeleteOneAsync(filter);

        return new DeleteDeviceResult(true);
    }
}
