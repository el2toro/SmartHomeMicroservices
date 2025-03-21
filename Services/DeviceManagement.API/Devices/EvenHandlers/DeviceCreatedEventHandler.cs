using DeviceManagement.API.Dtos;

namespace DeviceManagement.API.Devices.EvenHandlers;

public class DeviceCreatedEventHandler(IConfiguration configuration)
    : IConsumer<DeviceCreatedEvent>
{
    private readonly IConfiguration _configuration = configuration;
    public async Task Consume(ConsumeContext<DeviceCreatedEvent> context)
    {
        var client = new HttpClient();
        var mailEndpoint = _configuration["Endpoints:MailEndpoint"];

        await client.PostAsJsonAsync(mailEndpoint, BuildMessage(context.Message));
    }

    private MailDto BuildMessage(DeviceCreatedEvent deviceCreatedEvent)
    {
        return new MailDto("New device created", deviceCreatedEvent.DeviceAsJson.ToString());
    }
}
