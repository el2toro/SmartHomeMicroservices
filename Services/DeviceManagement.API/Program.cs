using Core.Behaviors;
using Core.Exceptions.Handler;
using Core.Messaging.MassTransit;
using DeviceManagement.API.Data.DbSettings;
using DeviceManagement.API.Repository;
using MassTransit.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.RegisterBsonSerializer();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IDeviceData, DeviceData>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();

