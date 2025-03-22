using Microsoft.AspNetCore.SignalR;

namespace DeviceManagement.API.Hubs;

public class DeviceHub : Hub
{
    public async Task SendMessage(bool isOn)
    {
        await Clients.All.SendAsync("ReceiveMessage", isOn);
    }
}
