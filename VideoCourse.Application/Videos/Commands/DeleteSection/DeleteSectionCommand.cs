using ErrorOr;
using MediatR;

namespace VideoCourse.Application.Videos.Commands.DeleteSection;

public record DeleteSectionCommand(Guid Id) : IRequest<ErrorOr<bool>>;