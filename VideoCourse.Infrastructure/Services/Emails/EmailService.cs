using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using VideoCourse.Application.Core.Abstractions.Emails;
using VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;
using VideoCourse.Application.Core.Abstractions.Emails.Settings;

namespace VideoCourse.Infrastructure.Services.Emails;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;

    public EmailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(IEmailRequest emailMessage)
    {
        var email = new MimeMessage()
        {
            From =
            {
                new MailboxAddress(_mailSettings.SenderDisplayName, _mailSettings.SenderEMail)
            },
            To =
            {
                MailboxAddress.Parse(emailMessage.EmailTo)
            },
            Subject = emailMessage.Subject,
            Body = new TextPart(TextFormat.Text)
            {
                Text = emailMessage.Body
            }
        };
        int port = int.Parse(_mailSettings.SmtpPort);
        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_mailSettings.SmtpServer, port, true);
        await smtpClient.AuthenticateAsync(_mailSettings.SmtpUsername, _mailSettings.SmtpPassword);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);

        return true;
    }

    public async Task<bool> SendWelcomeMessage(WelcomeEmailMessage message)
    {
        var emailToSend = new EmailRequest(
            emailTo: message.EmailTo,
            body: $"Hi {message.FirstName} {message.LastName} 🎉🎉🎉" +
                  $"Welcome to Video Course" +
                  $"We wish you a great learning experience",
            subject: $"{message.FirstName} {message.LastName} Welcome to Video Course"
        );
        return await SendEmailAsync(emailToSend);
    }
}