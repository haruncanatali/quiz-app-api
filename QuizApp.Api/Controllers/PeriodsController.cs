using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Periods.Commands.Create;
using QuizApp.Application.Periods.Commands.Delete;
using QuizApp.Application.Periods.Commands.Update;
using QuizApp.Application.Periods.Queries.Dtos;
using QuizApp.Application.Periods.Queries.GetPeriod;
using QuizApp.Application.Periods.Queries.GetPeriods;

namespace QuizApp.Api.Controllers;

public class PeriodsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<PeriodDto>>>> List([FromQuery] string? name)
    {
        return Ok(await Mediator.Send(new GetPeriodsQuery
        {
            Name = name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<PeriodDto>>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetPeriodQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromBody] CreatePeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromBody] UpdatePeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeletePeriodCommand { Id = id });
        return NoContent();
    }
}