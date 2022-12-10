using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired();

        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<PermissionRole>();

        builder.HasData(CreateRoles());

    }

    protected List<Role> CreateRoles()
    {
        List<Role> roles = new();
        roles.AddRange(
            Enum.GetValues<UserRole>()
                .Select(r => new Role((int)r, r.ToString())));
        
        return roles;
    }
}