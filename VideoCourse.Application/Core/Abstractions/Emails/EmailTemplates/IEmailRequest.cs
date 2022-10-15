namespace VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;

public interface IEmailRequest
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}