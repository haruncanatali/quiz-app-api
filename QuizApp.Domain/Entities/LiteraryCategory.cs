using QuizApp.Domain.Base;

namespace QuizApp.Domain.Entities;

public class LiteraryCategory : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Literary> Literaries { get; set; }
}