using DataProvidor.API.Providors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ITemperatureProvidor, TemperatureProvidor>();
builder.Services.AddTransient<IMotionDetectionProvidor, MotionDetectionProvidor>();

var app = builder.Build();

// Configure the HTTP request pipeline.

var temperatureProvidorService = app.Services.GetRequiredService<ITemperatureProvidor>();
var motionDetectionProvidor = app.Services.GetRequiredService<IMotionDetectionProvidor>();

app.MapGet("/indoorTemperature", () =>
{
    return temperatureProvidorService.GetIndoorTemperature();
});

app.MapGet("/outdoorTemperature", () =>
{
    return temperatureProvidorService.GetOutdoorTemperature();
});

app.MapGet("/checkMotionSensor", () =>
{
    return motionDetectionProvidor.CheckMotionSensor();
});

app.Run();
