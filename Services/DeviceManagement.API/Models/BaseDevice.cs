namespace DeviceManagement.API.Models;
public class BaseDevice
{
    public Guid DeviceId { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsOn { get; set; }
    public string Status { get; set; }
}
