namespace DeviceManagement.API.Devices.DeleteDevice;

public record DeleteDeviceCommand(Guid Id) : ICommand<DeleteDeviceResult>;
public record DeleteDeviceResult(bool IsSuccess);
internal class DeleteDeviceHandler(MongoDbContext dbContext)
    : ICommandHandler<DeleteDeviceCommand, DeleteDeviceResult>
{
    public async Task<DeleteDeviceResult> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", command.Id);

        await dbContext.DeviceCollection.DeleteOneAsync(filter);

        return new DeleteDeviceResult(true);
    }
}
