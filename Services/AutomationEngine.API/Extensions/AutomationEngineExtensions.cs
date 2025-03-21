using Core.Messaging.Events;
using MassTransit;

namespace AutomationEngine.API.Extensions;

public static class AutomationEngineExtensions
{
    public static void UseTemperatureMonitoring(this IApplicationBuilder app)
    {
        Timer timer = new Timer(async _ =>
        //Calls GetTemperature every 10 seconds
        await GetTemperature(app.ApplicationServices), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }

    private static async Task GetTemperature(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetAsync("https://localhost:7170/temperature");
            var temperature = await result.Content.ReadAsStringAsync();

            double convertedTemperature = Convert.ToDouble(temperature);
            if (convertedTemperature <= 15)
            {
                await publishEndpoint.Publish(new LowTemperatureEvent(convertedTemperature));
            }
            else if (convertedTemperature >= 25)
            {
                await publishEndpoint.Publish(new HighTemperatureEvent(convertedTemperature));
            }
        }
    }
}
