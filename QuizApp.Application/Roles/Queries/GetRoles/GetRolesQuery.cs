using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Roles.Queries.Dtos;

namespace QuizApp.Application.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<BaseResponseModel<List<RoleDto>>>
{
    public string? Name { get; set; }
}