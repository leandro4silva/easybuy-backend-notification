using MailKit.Net.Smtp;
using CompraFacil.Notification.Infra.Configurations;
using CompraFacil.Notification.Infra.SendGrid.Abstraction;
using MimeKit;

namespace CompraFacil.Notification.Infra.SendGrid;

public class SendMailService : ISendMailService
{
    private readonly MailConfiguration _config;
    private readonly ISmtpClient _sendSmtpClient;

    public SendMailService(ISmtpClient sendSmtpClient, MailConfiguration config)
    {
        _config = config;
        _sendSmtpClient = sendSmtpClient;
    }

    public async Task SendAsync(string subject, string content, string toEmail, string toName)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_config.FromName, _config.FromEmail));
        email.To.Add(new MailboxAddress(toName, toEmail));
        email.Subject = subject;

        email.Body = new TextPart("plain")
        {
            Text = content
        };

        await _sendSmtpClient.SendAsync(email);
    }
}
