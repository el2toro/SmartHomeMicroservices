namespace DeviceManagement.API.Models;

public class DoorLockDevice : BaseDevice<DoorLockDevice>
{
    public int UnlockSecret { get; set; }
}
