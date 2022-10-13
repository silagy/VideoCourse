using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.CreateSection;

public record CreateSectionCommand(
    string Name,
    string? Description,
    int StartTime,
    int EndTime,
    Guid VideoId) : IRequest<ErrorOr<VideoResponse>>;