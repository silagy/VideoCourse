using ErrorOr;
using MediatR;

namespace VideoCourse.Application.Videos.Commands.DeleteQuestion;

public record DeleteQuestionCommand(Guid Id, Guid VideoId): IRequest<ErrorOr<bool>>;