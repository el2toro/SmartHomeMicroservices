namespace DeviceManagement.API.Extensions;

public interface IMongoDbConfiguration
{
    IMongoCollection<BsonDocument> GetCollection();
}

public class MongoDbConfiguration : IMongoDbConfiguration
{
    private readonly IConfiguration _configuration;

    public MongoDbConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IMongoCollection<BsonDocument> GetCollection()
    {

        string connectionString = _configuration.GetSection("MongoDbSettings")["ConnectionString"]!;
        string databaseName = _configuration.GetSection("MongoDbSettings")["DatabaseName"]!;
        var collectionName = _configuration.GetSection("MongoDbSettings:Collections")["DevicesCollection"];

        return new MongoClient(connectionString)
            .GetDatabase(databaseName)
            .GetCollection<BsonDocument>(collectionName);
    }
}
