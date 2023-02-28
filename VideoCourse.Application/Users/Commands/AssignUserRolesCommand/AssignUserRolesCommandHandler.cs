using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.DomainErrors;

namespace VideoCourse.Application.Users.Commands.AssignUserRolesCommand;

public class AssignUserRolesCommandHandler : IRequestHandler<AssignUserRolesCommand, ErrorOr<UserFullResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserRolesCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UserFullResponse>> Handle(AssignUserRolesCommand request, CancellationToken cancellationToken)
    {
        // Get the user
        var userRequest = await _userRepository.GetByIdWithRolesAsync(request.UserId);

        if (userRequest.IsError) return userRequest.Errors;

        var user = userRequest.Value;
        
        // Get the roles
        var roles = await _userRepository.GetRolesById(request.Roles);

        if (!roles.Any()) return CustomErrors.Role.RolesNotFound;

        var addingRolesRequest = user.AddRoles(roles);

        if (addingRolesRequest.IsError) return addingRolesRequest.Errors;

        await _unitOfWork.Commit();

        return user.Adapt<UserFullResponse>();
    }
}