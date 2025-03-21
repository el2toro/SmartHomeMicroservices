namespace DeviceManagement.API.Repository;

public interface IDeviceRepository
{
    Task<BsonDocument> GetDeviceById(Guid deviceId);
    Task CreateDevice(JsonElement deviceAsJson, CancellationToken cancellationToken);
    Task DeleteDevice(Guid deviceId);
    Task<BsonDocument> UpdateDevice(JsonElement device, CancellationToken cancellationToken);
    Task<IAsyncCursor<BsonDocument>> GetDevices();
}
public class DeviceRepository(MongoDbContext dbContext, IDeviceData deviceData) : IDeviceRepository
{
    public async Task CreateDevice(JsonElement deviceAsJson, CancellationToken cancellationToken)
    {
        BsonDocument.TryParse(deviceAsJson.GetRawText(), out BsonDocument device);
        await dbContext.DeviceCollection.InsertOneAsync(device, new InsertOneOptions(), cancellationToken);
    }

    public async Task DeleteDevice(Guid deviceId)
    {
        var filter = GetFilterDefinition(deviceId);
        await dbContext.DeviceCollection.DeleteOneAsync(filter);
    }

    public async Task<BsonDocument> GetDeviceById(Guid deviceId)
    {
        var filter = GetFilterDefinition(deviceId);
        var result = await dbContext.DeviceCollection.Find(filter).FirstOrDefaultAsync();

        return result;
    }

    public async Task<IAsyncCursor<BsonDocument>> GetDevices()
    {
        return await dbContext.DeviceCollection.FindAsync(c => true);
    }

    public async Task<BsonDocument> UpdateDevice(JsonElement device, CancellationToken cancellationToken)
    {
        Guid deviceId = device.GetProperty(DeviceConstants.DEVICE_ID).GetGuid();

        var filter = GetFilterDefinition(deviceId);
        var updateDefinition = deviceData.GetUpdateDeviceDefinition(device);

        var result = await dbContext.DeviceCollection
            .FindOneAndUpdateAsync(filter, updateDefinition, cancellationToken: cancellationToken);

        return result;
    }

    private FilterDefinition<BsonDocument> GetFilterDefinition(Guid deviceId)
     => Builders<BsonDocument>.Filter.Eq(DeviceConstants.DEVICE_ID, deviceId.ToString());
}
