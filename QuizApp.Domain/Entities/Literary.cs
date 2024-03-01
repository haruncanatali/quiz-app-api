using QuizApp.Domain.Base;

namespace QuizApp.Domain.Entities;

public class Literary : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public long AuthorId { get; set; }
    public long LiteraryCategoryId { get; set; }
    public long PeriodId { get; set; }

    public Author Author { get; set; }
    public Period Period { get; set; }
    public LiteraryCategory LiteraryCategory { get; set; }
}