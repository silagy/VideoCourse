using MailKit.Net.Smtp;
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
        
        // Todo: return false or error when the email is failed to sent
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

    public async Task<bool> SendPublishedVideoMessage(VideoPublishedEmailMessage messageRequest)
    {
        var email = new EmailRequest(
            emailTo: messageRequest.EmailTo,
            subject: $"{messageRequest.VideoName} Has been published! 🎉",
            body: $"Hi {messageRequest.CreatorFullName}" +
                  Environment.NewLine +
                  $"The video '{messageRequest.VideoName}' has been published and it is now ready for learners"
        );

        return await SendEmailAsync(email);
    }
}