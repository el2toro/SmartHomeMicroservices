using DeviceManagement.API.Data;
using Core.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddCarter();

builder.Services.RegisterBsonSerializer();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IDeviceData, DeviceData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();

