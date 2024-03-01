using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Authors.Commands.Create;
using QuizApp.Application.Authors.Commands.Delete;
using QuizApp.Application.Authors.Commands.Update;
using QuizApp.Application.Authors.Queries.Dtos;
using QuizApp.Application.Authors.Queries.GetAuthor;
using QuizApp.Application.Authors.Queries.GetAuthors;
using QuizApp.Application.Common.Models;

namespace QuizApp.Api.Controllers;

public class AuthorsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<AuthorDto>>>> List([FromQuery] string? name, string? surname, long? periodId)
    {
        return Ok(await Mediator.Send(new GetAuthorsQuery
        {
            Name = name,
            Surname = surname,
            PeriodId = periodId
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<AuthorDto>>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetAuthorQuery { Id = id }));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromForm] CreateAuthorCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateAuthorCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteAuthorCommand { Id = id });
        return NoContent();
    }
}