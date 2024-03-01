using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Surname).IsRequired();
        builder.Property(c => c.Bio).IsRequired();
        builder.Property(c => c.PeriodId).IsRequired();
        builder.Property(c => c.Photo).IsRequired();
    }
}