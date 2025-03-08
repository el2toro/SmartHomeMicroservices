using DeviceManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddCarter();

builder.Services.AddSingleton<IMongoDbConfiguration, MongoDbConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

