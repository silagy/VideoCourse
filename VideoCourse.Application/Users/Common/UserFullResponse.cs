using System.Collections;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Users.Common;

public record UserFullResponse(Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IEnumerable<RoleResponse> Roles);