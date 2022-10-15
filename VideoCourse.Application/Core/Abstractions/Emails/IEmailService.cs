using VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;

namespace VideoCourse.Application.Core.Abstractions.Emails;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(IEmailRequest emailMessage);
    public Task<bool> SendWelcomeMessage(WelcomeEmailMessage message);
}