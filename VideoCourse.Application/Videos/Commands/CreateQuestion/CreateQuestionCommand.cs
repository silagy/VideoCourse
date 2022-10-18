using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.CreateQuestion;

public record CreateQuestionCommand(
    Guid Id,
    string Name,
    string? Feedback,
    string Content,
    int Time,
    Guid VideoId) : IRequest<ErrorOr<VideoResponse>>;