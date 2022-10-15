using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Type).IsRequired();
        builder.Property(o => o.Content).IsRequired();
        builder.Property(o => o.OccurredOnUtc).IsRequired();
        builder.Property(o => o.PublishedOnUtc);
        builder.Property(o => o.Error);
    }
}