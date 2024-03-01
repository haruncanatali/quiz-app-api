using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Persistence.Configurations;

public class LiteraryCategoryConfiguration : IEntityTypeConfiguration<LiteraryCategory>
{
    public void Configure(EntityTypeBuilder<LiteraryCategory> builder)
    {
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description).IsRequired();
    }
}