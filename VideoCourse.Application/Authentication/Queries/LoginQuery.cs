using MediatR;
using ErrorOr;
using VideoCourse.Application.Users.Common;

namespace VideoCourse.Application.Authentication.Queries;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<UserResponse>>;