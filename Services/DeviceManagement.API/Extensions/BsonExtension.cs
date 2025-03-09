using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace DeviceManagement.API.Extensions;

public static class BsonExtension
{
    public static IServiceCollection RegisterBsonSerializer(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        return services;
    }
}
