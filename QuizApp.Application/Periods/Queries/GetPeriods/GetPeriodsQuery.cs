using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Periods.Queries.Dtos;

namespace QuizApp.Application.Periods.Queries.GetPeriods;

public class GetPeriodsQuery : IRequest<BaseResponseModel<List<PeriodDto>>>
{
    public string? Name { get; set; }
}