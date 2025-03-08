namespace DeviceManagement.API.Models;

public class CameraDevice : BaseDevice<CameraDevice>
{
    public bool IsRecording { get; set; }
}
