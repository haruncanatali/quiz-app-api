using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Roles.Queries.Dtos;

public class RoleDto : IMapFrom<Role>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleDto>();
    }
}