using DataProvidor.API.Providors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ITemperatureProvidor, TemperatureProvidor>();

var app = builder.Build();

// Configure the HTTP request pipeline.

var temperatureProvidorService = app.Services.GetRequiredService<ITemperatureProvidor>();

app.MapGet("/temperature", () =>
{
    return temperatureProvidorService.GetTemperature();
});

app.Run();
