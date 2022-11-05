namespace VideoCourse.Application.Videos.Common;

public record BasicVideoResponse(
    Guid Id,
    string Url,
    string Name,
    string? Description,
    int Duration,
    Guid CreatorId,
    bool IsPublished,
    DateTime? PublishedOnUtc,
    DateTime CreationDate,
    DateTime UpdateDate);