using ErrorOr;
using MediatR;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Users.Commands.AssignUserRolesCommand;

public record AssignUserRolesCommand(Guid UserId, IEnumerable<UserRole> Roles) : IRequest<ErrorOr<UserFullResponse>>;