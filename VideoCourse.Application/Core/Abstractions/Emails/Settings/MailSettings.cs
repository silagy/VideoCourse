namespace VideoCourse.Application.Core.Abstractions.Emails.Settings;

public class MailSettings
{
    public const string SectionName = "MailSettings";
    
    public string SenderDisplayName { get; set; } = null!;
    public string SenderEMail { get; set; } = null!;
    public string SmtpUsername { get; set; } = null!;
    public string SmtpPassword { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public string SmtpPort { get; set; } = null!;
    
}