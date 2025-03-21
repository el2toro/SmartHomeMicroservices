using Core.Messaging.Events;
using MassTransit;

namespace AutomationEngine.API.EventHandlers;

public class LowTemperatureHandler : IConsumer<LowTemperatureEvent>
{
    public Task Consume(ConsumeContext<LowTemperatureEvent> context)
    {
        Console.WriteLine(context.Message);
        return Task.CompletedTask;
    }
}
