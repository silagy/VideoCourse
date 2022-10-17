using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Abstractions;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common.DbContextExtensions;

namespace VideoCourse.Infrastructure.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Notes");

        builder.HasKey(n => n.Id);

        builder.Property(note => note.Name).IsRequired();
        builder.Property(note => note.Content).IsRequired();
        builder.OwnsOne(note => note.Time, timeBuilder =>
        {
            timeBuilder.WithOwner();
            timeBuilder.Property(time => time.Value)
                .HasColumnName(nameof(Note.Time).ToSnakeCase())
                .IsRequired();
        });
        builder.Property(note => note.TypeId).IsRequired();
        
        builder.HasOne<Video>()
            .WithMany(note => note.Notes)
            .HasForeignKey(note => note.VideoId);

        builder.Property(note => note.CreationDate).IsRequired();
        builder.Property(note => note.UpdateDate);
        builder.Property(note => note.IsDeleted).HasDefaultValue(false);
        builder.Ignore(n => n.Type);
        builder.HasQueryFilter(note => !note.IsDeleted);
    }
}