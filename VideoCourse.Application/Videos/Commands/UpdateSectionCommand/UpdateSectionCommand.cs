using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.UpdateSectionCommand;

public record UpdateSectionCommand(string Name,
    string? Description,
    int StartTime,
    int EndTime,
    Guid VideoId,
    Guid SectionId) : IRequest<ErrorOr<VideoResponse>>;