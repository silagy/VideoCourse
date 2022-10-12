using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name).IsRequired();
        builder.Property(v => v.Description);

        builder.OwnsOne(v => v.Url, urlBuilder =>
        {
            urlBuilder.WithOwner();

            urlBuilder.Property(url => url.Value)
                .HasColumnName(nameof(Video.Url))
                .IsRequired();
        });

        builder.OwnsOne(v => v.Duration, durationBuilder =>
        {
            durationBuilder.WithOwner();

            durationBuilder.Property(duration => duration.Value)
                .HasColumnName(nameof(Video.Duration))
                .IsRequired();
        });

        builder.HasOne<User>(v => v.Creator)
            .WithOne()
            .HasForeignKey<Video>(v => v.CreatorId)
            .IsRequired();

        builder.HasMany<Section>(v => v.Sections);

        builder.Property(v => v.CreationDate).IsRequired();
        builder.Property(v => v.UpdateDate);
        builder.Property(v => v.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(v => !v.IsDeleted);
    }
}