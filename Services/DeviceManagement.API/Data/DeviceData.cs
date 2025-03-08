using DeviceManagement.API.Models;

namespace DeviceManagement.API.Data;

public interface IDeviceData
{
    UpdateDefinition<BsonDocument> GetUpdateDeviceDefinition(JsonElement deviceAsJson);
}

public class DeviceData : IDeviceData
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DeviceData()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
    }
    public UpdateDefinition<BsonDocument> GetUpdateDeviceDefinition(JsonElement deviceAsJson)
    {
        DeviceType deviceType = (DeviceType)deviceAsJson.GetProperty("deviceType").GetInt32();

        var updateDefinition = deviceType switch
        {
            DeviceType.Light => UpdateLightDevice(deviceAsJson),
            DeviceType.Camera => UpdateCameraDevice(deviceAsJson),
            DeviceType.Thermostat => UpdateTermostatDevice(deviceAsJson),
            DeviceType.DoorLock => UpdateDoorLookDevice(deviceAsJson),
            _ => throw new ArgumentOutOfRangeException("Device type not found")
        };

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateLightDevice(JsonElement deviceAsJson)
    {
        var lightDevice = JsonSerializer.Deserialize<LightDevice>(deviceAsJson, _jsonSerializerOptions);

        var updateDefinition = Builders<BsonDocument>.Update.Set("color", lightDevice?.Color)
        .Set("brightness", lightDevice?.Brightness)
        .Set("name", lightDevice?.Name)
        .Set("isOn", lightDevice?.IsOn)
        .Set("status", lightDevice?.Status);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateCameraDevice(JsonElement deviceAsJson)
    {
        CameraDevice cameraDevice = JsonSerializer.Deserialize<CameraDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = Builders<BsonDocument>.Update
        .Set("name", cameraDevice?.Name)
        .Set("isOn", cameraDevice?.IsOn)
        .Set("status", cameraDevice?.Status);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateTermostatDevice(JsonElement deviceAsJson)
    {
        TermostatDevice termostatDevice = JsonSerializer.Deserialize<TermostatDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = Builders<BsonDocument>.Update
        .Set("name", termostatDevice?.Name)
        .Set("isOn", termostatDevice?.IsOn)
        .Set("status", termostatDevice?.Status);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateDoorLookDevice(JsonElement deviceAsJson)
    {
        DoorLockDevice doorLockDevice = JsonSerializer.Deserialize<DoorLockDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = Builders<BsonDocument>.Update
        .Set("name", doorLockDevice?.Name)
        .Set("isOn", doorLockDevice?.IsOn)
        .Set("status", doorLockDevice?.Status);

        return updateDefinition;
    }
}
