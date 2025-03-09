using Core.Exceptions;

namespace DeviceManagement.API.Exceptions
{
    public class DeviceNotFoundException : NotFoundException
    {
        public DeviceNotFoundException(string id) : base("Device", id)
        {
        }
    }
}
