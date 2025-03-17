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
            DeviceType.Sensor => UpdateSensorDevice(deviceAsJson),
            _ => throw new ArgumentOutOfRangeException("Device type not found")
        };

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateLightDevice(JsonElement deviceAsJson)
    {
        var lightDevice = JsonSerializer.Deserialize<LightDevice>(deviceAsJson, _jsonSerializerOptions);

        var updateDefinition = CommonUpdate(deviceAsJson)
            .Set(nameof(lightDevice.Color), lightDevice?.Color)
            .Set(nameof(lightDevice.Brightness), lightDevice?.Brightness);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateCameraDevice(JsonElement deviceAsJson)
    {
        CameraDevice cameraDevice = JsonSerializer.Deserialize<CameraDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = CommonUpdate(deviceAsJson)
        .Set(nameof(cameraDevice.IsRecording), cameraDevice?.IsRecording);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateTermostatDevice(JsonElement deviceAsJson)
    {
        TermostatDevice termostatDevice = JsonSerializer.Deserialize<TermostatDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = CommonUpdate(deviceAsJson)
        .Set(nameof(termostatDevice.Temperature).ToCamelCase(), termostatDevice?.Temperature);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateDoorLookDevice(JsonElement deviceAsJson)
    {
        DoorLockDevice doorLockDevice = JsonSerializer.Deserialize<DoorLockDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = CommonUpdate(deviceAsJson)
        .Set(nameof(doorLockDevice.UnlockSecret).ToCamelCase(), doorLockDevice?.UnlockSecret);

        return updateDefinition;
    }

    private UpdateDefinition<BsonDocument> UpdateSensorDevice(JsonElement deviceAsJson)
    {
        SensorDevice sensorDevice = JsonSerializer.Deserialize<SensorDevice>(deviceAsJson, _jsonSerializerOptions)!;

        return CommonUpdate(deviceAsJson);
    }

    private UpdateDefinition<BsonDocument> CommonUpdate(JsonElement deviceAsJson)
    {
        BaseDevice baseDevice = JsonSerializer.Deserialize<BaseDevice>(deviceAsJson, _jsonSerializerOptions)!;

        var updateDefinition = Builders<BsonDocument>.Update
        .Set(nameof(baseDevice.Name).ToCamelCase(), baseDevice?.Name)
        .Set(nameof(baseDevice.IsOn).ToCamelCase(), baseDevice?.IsOn)
        .Set(nameof(baseDevice.Status).ToCamelCase(), baseDevice?.Status)
        .Set(nameof(baseDevice.DeviceType).ToCamelCase(), baseDevice?.DeviceType);

        return updateDefinition;
    }
}
