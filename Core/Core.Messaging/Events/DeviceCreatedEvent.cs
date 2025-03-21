using System.Text.Json;

namespace DeviceManagement.API.Devices.EvenHandlers;

public record DeviceCreatedEvent(JsonElement DeviceAsJson);

