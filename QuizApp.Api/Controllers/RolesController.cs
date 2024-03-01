using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Roles.Commands.AddToRole;
using QuizApp.Application.Roles.Commands.Create;
using QuizApp.Application.Roles.Commands.Update;
using QuizApp.Application.Roles.Queries.Dtos;
using QuizApp.Application.Roles.Queries.GetRoles;

namespace QuizApp.Api.Controllers;

public class RolesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<RoleDto>>>> List([FromQuery]string? name)
    {
        return Ok(await Mediator.Send(new GetRolesQuery
        {
            Name = name
        }));
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(CreateRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Route("AddToRole")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddToRole(AddToRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateRoleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}