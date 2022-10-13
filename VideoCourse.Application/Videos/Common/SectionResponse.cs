namespace VideoCourse.Application.Videos.Common;

public record SectionResponse(
    Guid Id,
    string Name,
    string? Description,
    int StartTime,
    int EndTime,
    DateTime CreationDate,
    DateTime UpdateDate);