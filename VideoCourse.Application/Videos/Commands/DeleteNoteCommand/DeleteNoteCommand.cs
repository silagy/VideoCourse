using ErrorOr;
using MediatR;

namespace VideoCourse.Application.Videos.Commands.DeleteNoteCommand;

public record DeleteNoteCommand(Guid Id, Guid VideoId): IRequest<ErrorOr<bool>>;