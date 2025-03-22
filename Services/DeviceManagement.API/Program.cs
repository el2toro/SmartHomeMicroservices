using Core.Behaviors;
using Core.Exceptions.Handler;
using Core.Messaging.MassTransit;
using DeviceManagement.API.Data.DbSettings;
using DeviceManagement.API.Hubs;
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
builder.Services.AddSignalR();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IDeviceData, DeviceData>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .WithOrigins("http://localhost:4200"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");
app.MapCarter();
app.UseExceptionHandler(options => { });

app.MapHub<DeviceHub>("/deviceNotification");

app.Run();

