using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Persistence.Configurations;

public class LiteraryConfiguration : IEntityTypeConfiguration<Literary>
{
    public void Configure(EntityTypeBuilder<Literary> builder)
    {
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.AuthorId).IsRequired();
        builder.Property(c => c.LiteraryCategoryId).IsRequired();
        builder.Property(c => c.PeriodId).IsRequired();
    }
}