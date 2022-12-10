using MediatR;
using VideoCourse.Application.Users.Common;

namespace VideoCourse.Application.Users.Queries.GetAllRoles;

public record GetRolesQuery : IRequest<IEnumerable<RoleResponse>>;