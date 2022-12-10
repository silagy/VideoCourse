using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name);
        builder.Ignore(p => p.PermissionBit);

        builder.HasData(CreateDate());

    }

    private List<Permission> CreateDate()
    {
        List<Permission> permissions = new();
        // Admin
        permissions.AddRange(Enum.GetValues<Permissions>().Select(p => Permission.Create(p).Value));

        return permissions;
    }
}