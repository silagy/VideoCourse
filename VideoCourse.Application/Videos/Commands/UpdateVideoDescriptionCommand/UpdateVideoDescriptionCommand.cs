using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.UpdateVideoDescriptionCommand;

public record UpdateVideoDescriptionCommand(
    Guid Id,
    string Name,
    string? Description): IRequest<ErrorOr<BasicVideoResponse>>;