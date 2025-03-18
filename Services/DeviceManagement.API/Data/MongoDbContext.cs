using DeviceManagement.API.Data.DbSettings;
using Microsoft.Extensions.Options;

namespace DeviceManagement.API.Data;

public class MongoDbContext
{
    private readonly IOptions<MongoDbSettings> _mongoDbSettings;
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
    {
        _mongoDbSettings = mongoDbSettings;

        var client = new MongoClient(_mongoDbSettings.Value.ConnectionString);
        _database = client.GetDatabase(_mongoDbSettings.Value.DatabaseName);
    }

    public IMongoCollection<BsonDocument> DeviceCollection =>
        _database.GetCollection<BsonDocument>(_mongoDbSettings.Value.Collections.First().Value);
}
