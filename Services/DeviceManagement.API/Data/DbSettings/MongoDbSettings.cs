namespace DeviceManagement.API.Data.DbSettings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public IDictionary<string, string> Collections { get; set; } = default!;
}
