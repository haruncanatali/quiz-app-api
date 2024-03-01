using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Commands.Create;
using QuizApp.Application.Literaries.Commands.Delete;
using QuizApp.Application.Literaries.Commands.Update;
using QuizApp.Application.Literaries.Queries.Dtos;
using QuizApp.Application.Literaries.Queries.GetLiteraries;
using QuizApp.Application.Literaries.Queries.GetLiterary;

namespace QuizApp.Api.Controllers;

public class LiterariesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<LiteraryDto>>>> List([FromQuery] string? name)
    {
        return Ok(await Mediator.Send(new GetLiterariesQuery
        {
            Name = name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<LiteraryDto>>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetLiteraryQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromBody] CreateLiteraryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromBody] UpdateLiteraryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteLiteraryCommand { Id = id });
        return NoContent();
    }
}