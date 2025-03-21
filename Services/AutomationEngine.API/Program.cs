using System.Reflection;
using AutomationEngine.API.Extensions;
using AutomationEngine.API.Settings;
using Core.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseTemperatureMonitoring(builder.Configuration.GetSection("Endpoints"));
app.UseMotionDetection(builder.Configuration.GetSection("Endpoints"));


app.MapGet("/engine", () =>
{

});

app.MapGet("/data", () =>
{

});

app.Run();

