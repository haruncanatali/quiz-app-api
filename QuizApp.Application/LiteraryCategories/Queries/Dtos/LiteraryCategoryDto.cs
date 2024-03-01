using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.LiteraryCategories.Queries.Dtos;

public class LiteraryCategoryDto : BaseDto, IMapFrom<LiteraryCategory>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<LiteraryForLiteraryCategoryDto> Literaries { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LiteraryCategory, LiteraryCategoryDto>()
            .ForMember(dest => dest.Literaries, opt =>
                opt.MapFrom(c=>c.Literaries));
    }
}