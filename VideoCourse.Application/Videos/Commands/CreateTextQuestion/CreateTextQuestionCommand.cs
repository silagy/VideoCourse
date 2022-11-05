using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Videos.Commands.CreateTextQuestion;

public record CreateTextQuestionCommand(
    Guid Id,
    string Name,
    string? Feedback,
    string Content,
    int Time,
    Guid VideoId) : IRequest<ErrorOr<VideoResponse>>;