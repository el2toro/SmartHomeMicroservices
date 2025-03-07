namespace DeviceManagement.API.Models;

public class LightDevice : BaseDevice<LightDevice>
{
    public string Color { get; set; }
    public string Brightness { get; set; }
}
