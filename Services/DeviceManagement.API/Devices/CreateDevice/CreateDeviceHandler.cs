using DeviceManagement.API.Devices.EvenHandlers;
using MassTransit;

namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement DeviceAsJson) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);

internal class CreateDeviceHandler(MongoDbContext dbContext, IPublishEndpoint publishEndpoint)
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

        BsonDocument.TryParse(command.DeviceAsJson.GetRawText(), out BsonDocument device);

        try
        {
            await publishEndpoint.Publish(new DeviceCreatedEvent() { Name = "event" }, cancellationToken);
        }
        catch (Exception ex)
        {

            throw;
        }

        await dbContext.DeviceCollection.InsertOneAsync(device, cancellationToken);

        return new CreateDeviceResult(true);
    }
}
