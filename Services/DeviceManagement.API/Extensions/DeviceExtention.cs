using DeviceManagement.API.Models;

namespace DeviceManagement.API.Extensions;

public static class DeviceExtention
{
    public static object ToDevice(this BsonDocument document)
    {
        DeviceType deviceType = (DeviceType)document.GetValue("deviceType").AsInt32;

        var jsonOption = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        object device = deviceType switch
        {
            DeviceType.Light => JsonSerializer.Deserialize<LightDevice>(document.ToJson(), jsonOption)!,
            DeviceType.Thermostat => JsonSerializer.Deserialize<Termostat>(document.ToJson(), jsonOption)!,
            DeviceType.Camera => JsonSerializer.Deserialize<Camera>(document.ToJson(), jsonOption)!,
            DeviceType.DoorLock => JsonSerializer.Deserialize<DoorLock>(document.ToJson(), jsonOption)!,
            _ => throw new Exception("Document type not found")
        };
        return device;
    }
}
