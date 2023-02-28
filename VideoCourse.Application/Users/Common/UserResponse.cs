using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Users.Common;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
    
    