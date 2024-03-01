using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Authors.Queries.Dtos;

public class AuthorDto : BaseDto, IMapFrom<Author>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Bio { get; set; }
    public string Photo { get; set; }
    public long PeriodId { get; set; }
    public string FullName { get; set; }
    public string PeriodName { get; set; }
    public List<LiteraryForAuthorDto> Literaries { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.PeriodName, opt =>
                opt.MapFrom(c => c.Period.Name))
            .ForMember(dest => dest.Literaries, opt =>
                opt.MapFrom(c => c.Literaries));
    }
}