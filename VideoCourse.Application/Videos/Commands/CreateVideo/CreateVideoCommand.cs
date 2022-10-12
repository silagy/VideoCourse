using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.CreateVideo;

public record CreateVideoCommand(
    string Url,
    string Name,
    string? Description,
    int Duration,
    Guid CreatorId
    ) : IRequest<ErrorOr<BasicVideoResponse>>;