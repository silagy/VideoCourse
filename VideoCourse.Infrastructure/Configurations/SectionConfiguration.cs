using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Configurations;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.HasKey(section => section.Id);

        builder.Property(section => section.Name).IsRequired();
        builder.Property(section => section.Description);
        builder.OwnsOne(section => section.StartTime, startTimeBuilder =>
        {
            startTimeBuilder.WithOwner();
            startTimeBuilder.Property(startTime => startTime.Value)
                .HasColumnName(nameof(Section.StartTime))
                .IsRequired();
        });
        
        builder.OwnsOne(section => section.EndTime, endTimeBuilder =>
        {
            endTimeBuilder.WithOwner();
            endTimeBuilder.Property(endTime => endTime.Value)
                .HasColumnName(nameof(Section.EndTime))
                .IsRequired();
        });

        builder.HasOne(s => s.Video)
            .WithOne()
            .HasForeignKey<Section>(s => s.VideoId)
            .IsRequired();
        
        builder.Property(section => section.CreationDate).IsRequired();
        builder.Property(section => section.UpdateDate);
        builder.Property(section => section.IsDeleted).HasDefaultValue(false);
        builder.HasQueryFilter(section => !section.IsDeleted);
    }
}