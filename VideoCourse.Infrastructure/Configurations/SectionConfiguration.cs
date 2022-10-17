using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common.DbContextExtensions;

namespace VideoCourse.Infrastructure.Configurations;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections")
            .HasKey(s => s.Id);

        builder.Property(a => a.Name).IsRequired();
        builder.Property(section => section.Description);
        builder.OwnsOne(section => section.StartTime, startTimeBuilder =>
        {
            startTimeBuilder.WithOwner();
            startTimeBuilder.Property(startTime => startTime.Value)
                .HasColumnName(nameof(Section.StartTime).ToSnakeCase())
                .IsRequired();
        });
        
        builder.OwnsOne(a => a.EndTime, endTimeBuilder =>
        {
            endTimeBuilder.WithOwner();
            endTimeBuilder.Property(endTime => endTime.Value)
                .HasColumnName(nameof(Section.EndTime).ToSnakeCase())
                .IsRequired();
        });

        builder.HasOne<Video>()
            .WithMany(s => s.Sections)
            .HasForeignKey(s => s.VideoId);

        builder.Property(section => section.CreationDate).IsRequired();
        builder.Property(section => section.UpdateDate);
        builder.Property(section => section.IsDeleted).HasDefaultValue(false);
        builder.HasQueryFilter(section => !section.IsDeleted);
    }
}