namespace DeviceManagement.API.Extensions;

public static class DeviceExtension
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
            DeviceType.Thermostat => JsonSerializer.Deserialize<TermostatDevice>(document.ToJson(), jsonOption)!,
            DeviceType.Camera => JsonSerializer.Deserialize<CameraDevice>(document.ToJson(), jsonOption)!,
            DeviceType.DoorLock => JsonSerializer.Deserialize<DoorLockDevice>(document.ToJson(), jsonOption)!,
            _ => throw new ArgumentOutOfRangeException("Device type doesn't exist")
        };
        return device;
    }
}
