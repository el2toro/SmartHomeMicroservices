using Core.Exceptions.Handler;
using HotelReservation.Services;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Dtos;
using Notification.API.Notifications;
using Notification.API.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IEmailNotification, EmailNotification>();
builder.Services.AddTransient<ISmsNotification, SmsNotification>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("TwilioSettings"));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

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
    await smsNotificationService.SendSmsAsync(smsDto);
});

app.UseExceptionHandler(options => { });

app.Run();