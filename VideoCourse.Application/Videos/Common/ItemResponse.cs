namespace VideoCourse.Application.Videos.Common;

public record ItemResponse(
    Guid Id,
    string Name,
    string? Content,
    int Duration,
    Guid VideoId,
    DateTime CreationDate,
    DateTime UpdateDate);