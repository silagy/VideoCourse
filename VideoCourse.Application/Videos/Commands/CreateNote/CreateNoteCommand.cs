using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.CreateNote;

public record CreateNoteCommand(
    string Name,
    string Content,
    int Time,
    Guid VideoId):IRequest<ErrorOr<VideoResponse>>;