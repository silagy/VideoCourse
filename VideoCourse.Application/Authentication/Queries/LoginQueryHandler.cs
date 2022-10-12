using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Authentication;
using VideoCourse.Application.Core.Abstractions.Cryptography;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Authentication.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IPasswordHashChecker _passwordHashChecker;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator, IPasswordHashChecker passwordHashChecker)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHashChecker = passwordHashChecker;
    }

    public async Task<ErrorOr<UserResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.Errors;
        }
        // Get the user
        var userResponse = await _userRepository.GetByEmailAsync(email.Value);
        if (userResponse.IsError)
        {
            return userResponse.Errors;
        }

        var user = userResponse.Value;
        // Validate the password
        if (!_passwordHashChecker.HashesMatch(user.Password, request.Password))
        {
            return Error.Failure(
                code: "User.PasswordInCorrect",
                description: "User password incorrect");
        }
        // Generate Token
        var token = _tokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
        // Return user
        var userToReturn = new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);

        return userToReturn;
    }
}