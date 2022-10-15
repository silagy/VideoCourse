namespace VideoCourse.Application.Core.Abstractions.Emails.Settings;

public class MailSettings
{
    public const string SectionName = "MailSettings";
    
    public string SenderDisplayName { get; set; }
    public string SenderEMail { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string SmtpServer { get; set; }
    public string SmtpPort { get; set; }
    
}