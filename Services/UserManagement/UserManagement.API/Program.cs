using Carter;
using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});

builder.Services.AddCarter();
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

