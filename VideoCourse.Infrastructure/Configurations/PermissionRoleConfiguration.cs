using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Infrastructure.Configurations;

public class PermissionRoleConfiguration : IEntityTypeConfiguration<PermissionRole>
{
    public void Configure(EntityTypeBuilder<PermissionRole> builder)
    {
        builder.HasKey(p => new { p.RoleId, p.PermissionId });

        builder.HasData(CreateData());
    }

    private List<PermissionRole> CreateData()
    {
        var permissionRoles = new List<PermissionRole>();
        // Admin permissions
        permissionRoles.AddRange(
            Enum.GetValues<Permissions>()
                .Select(p => new PermissionRole((int)UserRole.Admin, (int)p))
            );

        var usersPermissions = new List<Permissions>()
        {
            Permissions.EditUser,
            Permissions.ReadUser,
            Permissions.DeleteUser
        };
        permissionRoles.AddRange(
            usersPermissions
                .Select(p => new PermissionRole((int)UserRole.UsersManager, (int)p)));

        var videosPermissions = new List<Permissions>()
        {
            Permissions.ReadVideos,
            Permissions.EditVideos,
            Permissions.DeleteVideos
        };
        permissionRoles.AddRange(
            videosPermissions
                .Select(p => new PermissionRole((int)UserRole.VideosCreator, (int)p)));
        
        return permissionRoles;
    }
}