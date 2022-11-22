using SmartHome.Context;
using SmartHome.Interfaces;
using SmartHome.Models.Auth;
using SmartHome.Repository.Auth;
using SmartHome.Services;
using System.Text;
using SmartHome.Profiles;
using SmartHome.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Dependancies
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

builder.Services.AddDbContext<SmartHomeContext>();

builder.Services.AddAutoMapper(typeof(UserProfile));

var configuration = builder.Configuration;

// configure strongly typed settings object
var appSettingsSection = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// configure jwt authentication
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.UseRouting();

app.UseCors(options =>
     options.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
