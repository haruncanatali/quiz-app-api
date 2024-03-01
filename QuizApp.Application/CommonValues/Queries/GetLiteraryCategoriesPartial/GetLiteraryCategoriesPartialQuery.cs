using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetLiteraryCategoriesPartial;

public class GetLiteraryCategoriesPartialQuery : IRequest<BaseResponseModel<List<CommonValue>>>
{
    
}