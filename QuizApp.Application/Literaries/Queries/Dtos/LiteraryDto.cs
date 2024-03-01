using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Literaries.Queries.Dtos;

public class LiteraryDto : BaseDto, IMapFrom<Literary>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long AuthorId { get; set; }
    public long LiteraryCategoryId { get; set; }
    public long PeriodId { get; set; }

    public string AuthorFullName { get; set; }
    public string PeriodName { get; set; }
    public string LiteraryCategoryName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Literary, LiteraryDto>()
            .ForMember(dest => dest.AuthorFullName, opt =>
                opt.MapFrom(c => c.Author.FullName))
            .ForMember(dest => dest.PeriodName, opt =>
                opt.MapFrom(c => c.Period.Name))
            .ForMember(dest => dest.LiteraryCategoryName, opt =>
                opt.MapFrom(c => c.LiteraryCategory.Name));
    }
}