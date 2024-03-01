using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Authors.Queries.Dtos;

public class AuthorForPeriodDto : IMapFrom<Author>
{
    public long Id { get; set; }
    public string FullName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorForPeriodDto>()
            .ForMember(dest => dest.FullName, opt =>
                opt.MapFrom(c => c.FullName));
    }
}