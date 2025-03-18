using DeviceManagement.API.Devices.EvenHandlers;

namespace DeviceManagement.API.Devices.CreateDevice;
public record CreateDeviceCommand(JsonElement DeviceAsJson) : ICommand<CreateDeviceResult>;
public record CreateDeviceResult(bool IsSuccess);

public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceCommandValidator()
    {
        //TODO: Add more rules
        RuleFor(x => x.DeviceAsJson).NotNull().WithMessage("Device object is required");
    }
}

internal class CreateDeviceHandler(MongoDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateDeviceCommand, CreateDeviceResult>
{
    public async Task<CreateDeviceResult> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        Guid deviceId = command.DeviceAsJson.GetProperty(DeviceConstants.DEVICE_ID).GetGuid();

        var filter = Builders<BsonDocument>.Filter.Eq(DeviceConstants.DEVICE_ID, deviceId.ToString());
        var result = await dbContext.DeviceCollection.Find(filter).FirstOrDefaultAsync();

        if (result is not null)
        {
            throw new BadRequestException($"Device with id: {deviceId} already exists in database");
        }

        BsonDocument.TryParse(command.DeviceAsJson.GetRawText(), out BsonDocument device);
        await dbContext.DeviceCollection.InsertOneAsync(device, new InsertOneOptions(), cancellationToken);

        await publishEndpoint.Publish(new DeviceCreatedEvent(command.DeviceAsJson), cancellationToken);

        return new CreateDeviceResult(true);
    }
}
