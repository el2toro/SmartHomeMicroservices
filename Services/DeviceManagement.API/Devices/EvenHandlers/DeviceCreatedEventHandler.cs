using MassTransit;

namespace DeviceManagement.API.Devices.EvenHandlers;

public class DeviceCreatedEventHandler() : IConsumer<DeviceCreatedEvent>
{
    public Task Consume(ConsumeContext<DeviceCreatedEvent> context)
    {
        var client = new HttpClient();


        client.GetAsync("https://localhost:7125/email");

        return Task.CompletedTask;
    }
}
