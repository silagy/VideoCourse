namespace VideoCourse.Application.Videos.Common;

public record VideoResponse(
    Guid Id,
    string Url,
    string Name,
    string? Description,
    int Duration,
    Guid CreatorId,
    DateTime CreationDate,
    DateTime UpdateDate,
    IEnumerable<SectionResponse> Sections);