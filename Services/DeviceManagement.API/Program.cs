using DeviceManagement.API.Configuration;
using DeviceManagement.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddCarter();

builder.Services.AddSingleton<IMongoDbConfiguration, MongoDbConfiguration>();
builder.Services.AddScoped<IDeviceData, DeviceData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();

