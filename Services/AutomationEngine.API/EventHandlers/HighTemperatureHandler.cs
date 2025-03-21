using AutomationEngine.API.Dtos;
using System.Text.Json;

namespace AutomationEngine.API.EventHandlers;

public class HighTemperatureHandler(IConfiguration configuration) : IConsumer<HighTemperatureEvent>
{
    JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public async Task Consume(ConsumeContext<HighTemperatureEvent> context)
    {
        HttpClient httpClient = new HttpClient();

        await TurnAirConditionerOn(httpClient);
        await SendNotification(httpClient);
    }

    private async Task TurnAirConditionerOn(HttpClient httpClient)
    {
        var airConditioner = new AirConditionerDto
        {
            DeviceId = Guid.Parse("357d5793-719a-5c72-bfb7-1d99ee740f92"),
            IsOn = true,
            Temperature = 22,
            DeviceType = DeviceType.AirConditioner,
            Fan = 50
        };

        var content = new StringContent(JsonSerializer.Serialize(airConditioner, options), System.Text.Encoding.UTF8, "application/json");


        await httpClient.PutAsync(configuration["Endpoints:DeviceManagement_UpdateDevice"], content);
    }

    private async Task SendNotification(HttpClient httpClient)
    {
        MailDto mailDto = new("Air conditioner", "Air conditioner was turned on");
        var content = new StringContent(JsonSerializer.Serialize(mailDto, options), System.Text.Encoding.UTF8, "application/json");
        await httpClient.PostAsync(configuration["Endpoints:MailEndpoint"], content);
    }
}
