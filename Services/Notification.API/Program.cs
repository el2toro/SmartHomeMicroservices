using HotelReservation.Services;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Dtos;
using Notification.API.Notifications;
using Notification.API.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IEmailNotification, EmailNotification>();
builder.Services.AddTransient<ISmsNotification, SmsNotification>();

IConfiguration configuration = builder.Configuration;

builder.Services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
builder.Services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
builder.Services.Configure<TwilioSettings>(configuration.GetSection("TwilioSettings"));

var app = builder.Build();

var smsNotificationService = app.Services.GetRequiredService<ISmsNotification>();
var emailNotificationService = app.Services.GetRequiredService<IEmailNotification>();

// Configure the HTTP request pipeline.

//Endpoints
app.MapPost("/mail", async ([FromBody] MailDto mail) =>
{
    await emailNotificationService.SendEmailAsync(mail);
});

app.MapPost("/sms", async ([FromBody] SmsDto smsDto) =>
{
    await smsNotificationService.SendSmsAsync(smsDto.FromNumber, smsDto.Message);
});

app.Run();