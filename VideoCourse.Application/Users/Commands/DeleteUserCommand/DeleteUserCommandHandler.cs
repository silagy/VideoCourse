using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Events;

namespace VideoCourse.Application.Users.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Get the user
        var userResponse = await _userRepository.GetByIdAsync(request.Id);

        if (userResponse.IsError) return userResponse.Errors;

        if (userResponse.Value is null)
        {
            return CustomErrors.Entity.EntityNotFound;
        }

        var user = userResponse.Value;
        
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        var result = await _userRepository.Remove(user);
        await _unitOfWork.Commit();

        return result;
    }
}