using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;

public class VideoPublishedEmailMessage : IEmailRequest
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public Guid VideoId { get; set; }

    public VideoUrl Url { get; set; }

    public string VideoName { get; set; }

    public string CreatorFirstName { get; set; }

    public string CreatorLastName { get; set; }

    public string CreatorFullName => $"{CreatorFirstName} {CreatorLastName}";
    
    public VideoPublishedEmailMessage(string emailTo, Guid videoId, VideoUrl url, string creatorFirstName, string creatorLastName, string videoName)
    {
        EmailTo = emailTo;
        VideoId = videoId;
        Url = url;
        CreatorFirstName = creatorFirstName;
        CreatorLastName = creatorLastName;
        VideoName = videoName;
    }
}