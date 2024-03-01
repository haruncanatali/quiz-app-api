using AutoMapper;
using QuizApp.Application.Authors.Queries.Dtos;
using QuizApp.Application.Common.Mappings;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Periods.Queries.Dtos;

public class PeriodDto : BaseDto, IMapFrom<Period>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<AuthorForPeriodDto> Authors { get; set; }
    public List<LiteraryForPeriodDto> Literaries { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Period, PeriodDto>()
            .ForMember(dest => dest.Authors, opt =>
                opt.MapFrom(c => c.Authors))
            .ForMember(dest => dest.Literaries, opt =>
                opt.MapFrom(c => c.Literaries));
    }
}