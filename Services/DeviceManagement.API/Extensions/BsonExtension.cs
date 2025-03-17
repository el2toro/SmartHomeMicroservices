using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace DeviceManagement.API.Extensions;

public static class BsonExtension
{
    public static IServiceCollection RegisterBsonSerializer(this IServiceCollection services)
    {
        //TODO: remove return type, there is no need to return services as there are no new services added

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        return services;
    }
}
