using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Users.Common;

namespace VideoCourse.Application.Users.Queries.GetAllRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetRolesQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<RoleResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        // Get all roles

        var roles = await _userRepository.GetAllRoles();

        return roles.Adapt<IEnumerable<RoleResponse>>();
    }
}