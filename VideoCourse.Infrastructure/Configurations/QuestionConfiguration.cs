using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common.DbContextExtensions;

namespace VideoCourse.Infrastructure.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions")
            .HasKey(question => question.Id);


        builder.Property(question => question.Name).IsRequired();
        builder.Property(question => question.Content).IsRequired();
        builder.Property(question => question.Feedback);
        builder.OwnsOne(question => question.Time, timeBuilder =>
        {
            timeBuilder.WithOwner();
            timeBuilder.Property(time => time.Value)
                .HasColumnName(nameof(Question.Time).ToSnakeCase())
                .IsRequired();
        });
        builder.Property(question => question.TypeId).IsRequired();
        
        builder.HasOne<Video>()
            .WithMany(question => question.Questions)
            .HasForeignKey(question => question.VideoId);

        builder.HasMany<QuestionOption>()
            .WithOne()
            .HasForeignKey(q => q.QuestionId);

        builder.Property(question => question.CreationDate).IsRequired();
        builder.Property(question => question.UpdateDate);
        builder.Property(question => question.IsDeleted).HasDefaultValue(false);
        builder.Ignore(n => n.Type);
        builder.HasQueryFilter(question => !question.IsDeleted);
    }
}