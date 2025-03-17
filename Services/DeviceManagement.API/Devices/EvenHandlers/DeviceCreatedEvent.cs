namespace DeviceManagement.API.Devices.EvenHandlers;

public class DeviceCreatedEvent
{
    public Guid DeviceId { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsOn { get; set; }
    public string Status { get; set; }
}
