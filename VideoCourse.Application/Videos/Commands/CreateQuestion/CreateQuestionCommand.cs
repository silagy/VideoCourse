using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Videos.Commands.CreateQuestion;

public record CreateQuestionCommand(
    Guid Id,
    string Name,
    string? Feedback,
    string Content,
    int Time,
    Guid VideoId,
    QuestionType Type,
    IEnumerable<CreateQuestionOption> Options) : IRequest<ErrorOr<VideoResponse>>;