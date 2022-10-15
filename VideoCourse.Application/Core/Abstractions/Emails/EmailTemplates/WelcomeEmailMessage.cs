using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;

public sealed class WelcomeEmailMessage : IEmailRequest
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public WelcomeEmailMessage(string emailTo, string firstName, string lastName)
    {
        EmailTo = emailTo;
        FirstName = firstName;
        LastName = lastName;
    }
}