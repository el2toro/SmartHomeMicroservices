namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement DeviceAsJson) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);

internal class CreateDeviceHandler(MongoDbContext dbContext)
    : ICommandHandler<CreateDeviceCommand, CreateDeviceResult>
{
    public async Task<CreateDeviceResult> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        //TODO: remove harcoded deviceId
        //Add fluent validation
        Guid deviceId = command.DeviceAsJson.GetProperty("deviceId").GetGuid();

        var filter = Builders<BsonDocument>.Filter.Eq("deviceId", deviceId.ToString());
        var result = await dbContext.DeviceCollection.Find(filter).FirstOrDefaultAsync();

        if (result is not null)
        {
            throw new BadRequestException($"Device with id: {deviceId} already exists in database");
        }


        await dbContext.DeviceCollection.InsertOneAsync(BsonDocument.Parse(command.DeviceAsJson.GetRawText()), cancellationToken);

        return new CreateDeviceResult(true);
    }
}
