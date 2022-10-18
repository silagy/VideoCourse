using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Configurations;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable("QuestionOptions")
            .HasKey(q => q.Id);

        builder.Property(q => q.Name)
            .IsRequired();
        builder.Property(q => q.IsRight)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasOne<Question>()
            .WithMany(q => q.QuestionOptions)
            .HasForeignKey(q => q.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(q => q.CreationDate).IsRequired();
        builder.Property(q => q.UpdateDate);
        builder.Property(q => q.IsDeleted).HasDefaultValue(false);
        builder.HasQueryFilter(question => !question.IsDeleted);
        
    }
}