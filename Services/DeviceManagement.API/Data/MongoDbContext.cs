namespace DeviceManagement.API.Data;

public class MongoDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _database;
    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _collectionName;

    public MongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        _connectionString = _configuration.GetSection("MongoDbSettings")["ConnectionString"]!;
        _databaseName = _configuration.GetSection("MongoDbSettings")["DatabaseName"]!;
        _collectionName = _configuration.GetSection("MongoDbSettings:Collections")["DevicesCollection"]!;

        var client = new MongoClient(_connectionString);
        _database = client.GetDatabase(_databaseName);
    }

    // Add properties for each of your collections, for example:
    public IMongoCollection<BsonDocument> DeviceCollection =>
        _database.GetCollection<BsonDocument>(_collectionName);
}
