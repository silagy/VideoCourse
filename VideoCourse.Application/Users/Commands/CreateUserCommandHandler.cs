using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Authentication;
using VideoCourse.Application.Core.Abstractions.Cryptography;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ErrorOr<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.FirstError;
        }
        
        // Create new user instance
        var user = new User(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            email.Value,
            _passwordHasher.HashPassword(request.Password),
            request.Role);

        var results = await _userRepository.Create(user);

        if (results.IsError)
        {
            return results.Errors;
        }
        await _unitOfWork.Commit();

        var token = _tokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }
}