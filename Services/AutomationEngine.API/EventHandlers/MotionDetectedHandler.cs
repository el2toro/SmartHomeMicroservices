using AutomationEngine.API.Dtos;

namespace AutomationEngine.API.EventHandlers;

public class MotionDetectedHandler(IConfiguration configuration) : IConsumer<MotionDetectedEvent>
{
    private readonly IConfiguration _configuration = configuration;
    public async Task Consume(ConsumeContext<MotionDetectedEvent> context)
    {
        var client = new HttpClient();
        var mailEndpoint = _configuration["Endpoints:MailEndpoint"];

        await client.PostAsJsonAsync(mailEndpoint, BuildMessage());
    }

    private MailDto BuildMessage()
    {
        return new MailDto("Motion detected", $"A motion was detected in the room at: {DateTime.Now}");
    }
}
