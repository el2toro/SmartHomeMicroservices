using Core.Messaging.Events;
using MassTransit;

namespace AutomationEngine.API.EventHandlers;

public class HighTemperatureHandler : IConsumer<HighTemperatureEvent>
{
    public Task Consume(ConsumeContext<HighTemperatureEvent> context)
    {
        Console.WriteLine(context.Message);
        return Task.CompletedTask;
    }
}
