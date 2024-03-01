using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetPeriodsPartial;

public class GetPeriodsPartialQuery : IRequest<BaseResponseModel<List<CommonValue>>>
{
    
}