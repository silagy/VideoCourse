using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Users.Common;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role,
    string Token);