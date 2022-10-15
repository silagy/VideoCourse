using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.PublishVideoCommand;

public record PublishVideoCommand(Guid Id): IRequest<ErrorOr<BasicVideoResponse>>;