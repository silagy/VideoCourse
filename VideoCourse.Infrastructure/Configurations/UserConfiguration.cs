using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common.DbContextExtensions;

namespace VideoCourse.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users")
            .HasKey(u => u.Id);

        builder.Property(user => user.FirstName).IsRequired();
        builder.Property(user => user.LastName).IsRequired();
        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();
            emailBuilder.Property(email => email.Value)
                .HasColumnName(nameof(User.Email).ToSnakeCase())
                .IsRequired();
        });
        builder.Property(user => user.Password)
            .IsRequired();

        builder.Property(user => user.RoleId).IsRequired();
        builder.Property(user => user.CreationDate).IsRequired();
        builder.Property(user => user.UpdateDate);
        builder.Property(user => user.IsDeleted).HasDefaultValue(false);
        builder.Ignore(user => user.Role);

        builder.HasQueryFilter(user => !user.IsDeleted);
    }
}