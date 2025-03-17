using Microsoft.Extensions.Options;
using Notification.API.Dtos;
using Notification.API.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HotelReservation.Services;

public interface ISmsNotification
{
    Task<string> SendSmsAsync(SmsDto smsDto);
}
public class SmsNotification(IOptions<TwilioSettings> smsOptions,
    ILogger<ISmsNotification> logger) : ISmsNotification
{
    private readonly IOptions<TwilioSettings> _smsOptions = smsOptions;
    private readonly ILogger<ISmsNotification> _logger = logger;

    public async Task<string> SendSmsAsync(SmsDto smsDto)
    {
        TwilioClient.Init(_smsOptions.Value.AccountSid, _smsOptions.Value.AuthToken);

        var response = await MessageResource.CreateAsync(
        body: smsDto.Message,
        from: new Twilio.Types.PhoneNumber(_smsOptions.Value.FromNumber),
        to: new Twilio.Types.PhoneNumber(smsDto.ToNumber));

        return response.Body;
    }
}
