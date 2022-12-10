using Microsoft.AspNetCore.Authorization;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Infrastructure.Common.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permissions)
    :base(policy: permissions.ToString())
    {
    }
}