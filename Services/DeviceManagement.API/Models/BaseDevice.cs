namespace DeviceManagement.API.Models;
public class BaseDevice<T> where T : class
{
    public string DeviceId { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsOn { get; set; }
    public string Status { get; set; }
}

public enum DeviceType
{
    Light,
    Thermostat,
    Camera,
    DoorLock
}
