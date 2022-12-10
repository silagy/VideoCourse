using Mapster;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Users.Mappings;

public class RolesResponsesMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, RoleResponse>();
    }
}