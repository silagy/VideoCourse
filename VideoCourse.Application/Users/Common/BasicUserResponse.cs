using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Users.Common;

public record BasicUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role,
    DateTime CreationDate,
    DateTime UpdateDate);