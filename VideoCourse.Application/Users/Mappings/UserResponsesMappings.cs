using Mapster;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Users.Mappings;

public class UserResponsesMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Email, src => src.Email.Value);
        config.NewConfig<User, BasicUserResponse>()
            .Map(dest => dest.Email, src => src.Email.Value);

        config.NewConfig<User, UserFullResponse>()
            .Map(dest => dest.Email, src => src.Email.Value)
            .Map(dest => dest.Roles, src => src.Roles);
    }
}