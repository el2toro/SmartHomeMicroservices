using System.Text.Json;

namespace AutomationEngine.API.Extensions;

public static class AutomationEngineExtensions
{
    public static void UseTemperatureMonitoring(this IApplicationBuilder app,
        IConfigurationSection configurationSection)
    {
        Timer timer = new Timer(async _ =>
        //Calls GetTemperature every 10 seconds
        await GetIndoorTemperature(app.ApplicationServices, configurationSection),
        null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
    }

    public static void UseMotionDetection(this IApplicationBuilder app,
        IConfigurationSection configurationSection)
    {
        Timer timer = new Timer(async _ =>
        //Calls CheckMotionSensor every 10 seconds
        await CheckMotionSensor(app.ApplicationServices, configurationSection),
        null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }

    private static async Task GetIndoorTemperature(IServiceProvider serviceProvider,
        IConfigurationSection configurationSection)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            var result = await GetResult(configurationSection["IndoorTemperature"]!);
            double temperature = JsonSerializer.Deserialize<double>(result);

            if (temperature <= 15)
            {
                await publishEndpoint.Publish(new LowTemperatureEvent(temperature));
            }
            else if (temperature >= 25)
            {
                await publishEndpoint.Publish(new HighTemperatureEvent(temperature));
            }
        }
    }

    private static async Task CheckMotionSensor(IServiceProvider serviceProvider,
        IConfigurationSection configurationSection)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            var result = await GetResult(configurationSection["MotionSensor"]!);
            bool motionDetected = JsonSerializer.Deserialize<bool>(result);

            if (motionDetected)
            {
                await publishEndpoint.Publish(new MotionDetectedEvent(true));
            }
        }
    }

    private static async Task<string> GetResult(string endpoint)
    {
        HttpClient httpClient = new HttpClient();
        var response = await httpClient.GetAsync(endpoint);
        return await response.Content.ReadAsStringAsync();
    }
}
