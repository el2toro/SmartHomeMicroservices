using Microsoft.Extensions.Options;
using Notification.API.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HotelReservation.Services;

public interface ISmsNotification
{
    Task<string> SendSmsAsync(string toNumber, string message);
}
public class SmsNotification(IOptions<TwilioSettings> smsOptions,
    ILogger<ISmsNotification> logger) : ISmsNotification
{
    private readonly IOptions<TwilioSettings> _smsOptions = smsOptions;
    private readonly ILogger<ISmsNotification> _logger = logger;

    public async Task<string> SendSmsAsync(string toNumber, string message)
    {
        TwilioClient.Init(_smsOptions.Value.AccountSid, _smsOptions.Value.AuthToken);

        try
        {
            var response = await MessageResource.CreateAsync(
            body: message,
            from: new Twilio.Types.PhoneNumber(_smsOptions.Value.FromNumber),
            to: new Twilio.Types.PhoneNumber(toNumber));

            return response.Body;
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong while trying to send sms", ex);
            throw;
        }
    }
}
