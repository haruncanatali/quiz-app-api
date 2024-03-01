using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Periods.Queries.Dtos;

namespace QuizApp.Application.Periods.Queries.GetPeriod;

public class GetPeriodQuery : IRequest<BaseResponseModel<PeriodDto>>
{
    public long Id { get; set; }
}