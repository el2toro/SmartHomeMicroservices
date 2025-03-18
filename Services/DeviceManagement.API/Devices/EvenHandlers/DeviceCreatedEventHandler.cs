using MassTransit;

namespace DeviceManagement.API.Devices.EvenHandlers;

public class DeviceCreatedEventHandler(IConfiguration configuration)
    : IConsumer<DeviceCreatedEvent>
{
    private readonly IConfiguration _configuration = configuration;
    public async Task Consume(ConsumeContext<DeviceCreatedEvent> context)
    {
        var client = new HttpClient();
        var mailEndpoint = _configuration.GetSection("Endpoints:MailEndpoint").Value;

        await client.PostAsJsonAsync(mailEndpoint, context.Message);
    }
}
