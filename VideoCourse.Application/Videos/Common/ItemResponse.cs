using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Videos.Common;

public record ItemResponse(
    Guid Id,
    string Name,
    string? Content,
    int Duration,
    ItemType Type,
    Guid VideoId,
    DateTime CreationDate,
    DateTime UpdateDate);