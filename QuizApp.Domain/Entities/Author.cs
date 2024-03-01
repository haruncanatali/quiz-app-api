using System.Runtime.Serialization;
using QuizApp.Domain.Base;
using QuizApp.Domain.Base.Abstract;

namespace QuizApp.Domain.Entities;

public class Author : BaseEntity, IEntityWithPhoto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Bio { get; set; }
    public string Photo { get; set; }

    public long PeriodId { get; set; }

    public Period Period { get; set; }
    public List<Literary> Literaries { get; set; }
    
    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{Name} {Surname}"; }
    }
}