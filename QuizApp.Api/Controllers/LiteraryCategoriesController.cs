using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.GetLiterary;
using QuizApp.Application.LiteraryCategories.Commands.Create;
using QuizApp.Application.LiteraryCategories.Commands.Delete;
using QuizApp.Application.LiteraryCategories.Commands.Update;
using QuizApp.Application.LiteraryCategories.Queries.Dtos;
using QuizApp.Application.LiteraryCategories.Queries.GetLiteraryCategories;
using QuizApp.Application.LiteraryCategories.Queries.GetLiteraryCategory;

namespace QuizApp.Api.Controllers;

public class LiteraryCategoriesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<LiteraryCategoryDto>>>> List([FromQuery] string? name)
    {
        return Ok(await Mediator.Send(new GetLiteraryCategoriesQuery
        {
            Name = name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<LiteraryCategoryDto>>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetLiteraryCategoryQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromBody] CreateLiteraryCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromBody] UpdateLiteraryCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteLiteraryCategoryCommand { Id = id });
        return NoContent();
    }
}