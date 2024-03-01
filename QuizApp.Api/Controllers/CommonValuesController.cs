using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;
using QuizApp.Application.CommonValues.Queries.GetAuthorsPartial;
using QuizApp.Application.CommonValues.Queries.GetGenders;
using QuizApp.Application.CommonValues.Queries.GetLiteraryCategoriesPartial;
using QuizApp.Application.CommonValues.Queries.GetPeriodsPartial;
using QuizApp.Application.CommonValues.Queries.GetStatistics;

namespace QuizApp.Api.Controllers;

public class CommonValuesController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    [Route("Genders")]
    public async Task<ActionResult<BaseResponseModel<List<CommonValue>>>> GetGenders()
    {
        return Ok(await Mediator.Send(new GetGendersQuery()));
    }
    
    [HttpGet]
    [Route("Authors")]
    public async Task<ActionResult<BaseResponseModel<List<CommonValue>>>> GetAuthorsPartial()
    {
        return Ok(await Mediator.Send(new GetAuthorsPartialQuery()));
    }
    
    [HttpGet]
    [Route("Periods")]
    public async Task<ActionResult<BaseResponseModel<List<CommonValue>>>> GetPeriodsPartial()
    {
        return Ok(await Mediator.Send(new GetPeriodsPartialQuery()));
    }
    
    [HttpGet]
    [Route("LiteraryCategories")]
    public async Task<ActionResult<BaseResponseModel<List<CommonValue>>>> GetLiteraryCategoriesPartial()
    {
        return Ok(await Mediator.Send(new GetLiteraryCategoriesPartialQuery()));
    }

    [HttpGet]
    [Route("Statistics")]
    public async Task<ActionResult<BaseResponseModel<StatisticDto>>> GetStatistics()
    {
        return Ok(await Mediator.Send(new GetStatisticsQuery()));
    }
}