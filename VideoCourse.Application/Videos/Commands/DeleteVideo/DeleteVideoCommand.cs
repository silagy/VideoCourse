using ErrorOr;
using MediatR;

namespace VideoCourse.Application.Videos.Commands.DeleteVideo;

public record DeleteVideoCommand(Guid Id): IRequest<ErrorOr<bool>>;