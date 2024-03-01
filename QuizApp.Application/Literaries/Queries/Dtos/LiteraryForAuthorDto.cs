using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Literaries.Queries.Dtos;

public class LiteraryForAuthorDto : IMapFrom<Literary>
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Literary, LiteraryForAuthorDto>();
    }
}