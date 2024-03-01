using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetStatistics;

public class GetStatisticsQuery : IRequest<BaseResponseModel<StatisticDto>>
{
    
}