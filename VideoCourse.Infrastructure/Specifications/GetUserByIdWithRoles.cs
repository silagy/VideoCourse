using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public sealed class GetUserByIdWithRoles : Specification<User>
{
    public GetUserByIdWithRoles(Guid UserId)
    :base (u => u.Id == UserId)
    {
        Includes.Add(u => u.Roles);
    }
}