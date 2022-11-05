using ErrorOr;
using MediatR;

namespace VideoCourse.Application.Users.Commands.DeleteUserCommand;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<bool>>;