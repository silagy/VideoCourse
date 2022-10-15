namespace VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;

public class EmailRequest : IEmailRequest
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public EmailRequest(string emailTo, string subject, string body)
    {
        EmailTo = emailTo;
        Subject = subject;
        Body = body;
    }
}