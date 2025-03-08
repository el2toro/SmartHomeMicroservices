namespace DeviceManagement.API.Models;
public class BaseDevice<T> where T : class
{
    public int DeviceId { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsOn { get; set; }
    public string Status { get; set; }
}

public enum DeviceType
{
    Light = 1,
    Thermostat = 2,
    Camera = 3,
    DoorLock = 4
}
