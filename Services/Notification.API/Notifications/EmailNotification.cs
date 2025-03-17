using Microsoft.Extensions.Options;
using Notification.API.Dtos;
using Notification.API.Settings;
using System.Net;
using System.Net.Mail;

namespace Notification.API.Notifications;

public interface IEmailNotification
{
    Task SendEmailAsync(MailDto mailDto);
}
public class EmailNotification
    (IOptions<MailSettings> emailOptions, IOptions<SmtpSettings> smtpOptions)
    : IEmailNotification
{
    private readonly IOptions<MailSettings> _emailOptions = emailOptions;
    private readonly IOptions<SmtpSettings> _smtpOptions = smtpOptions;
    public async Task SendEmailAsync(MailDto mailDto)
    {
        MailMessage mailMessage = GetMailMessage(mailDto);
        using SmtpClient smtpClient = GetSmtpClient();

        await smtpClient.SendMailAsync(mailMessage);
    }

    private MailMessage GetMailMessage(MailDto mailDto)
    {
        MailMessage mailMessage = new()
        {
            From = new MailAddress(_emailOptions.Value.From),
            Subject = mailDto.Subject,
            Body = mailDto.Body
        };

        mailMessage.To.Add(_emailOptions.Value.To);

        return mailMessage;
    }

    private SmtpClient GetSmtpClient()
    {
        SmtpClient smtpClient = new()
        {
            Host = _smtpOptions.Value.Host,
            Port = _smtpOptions.Value.Port,
            Credentials = new NetworkCredential(_smtpOptions.Value.UserName, _smtpOptions.Value.Password),
            EnableSsl = true
        };

        return smtpClient;
    }
}
