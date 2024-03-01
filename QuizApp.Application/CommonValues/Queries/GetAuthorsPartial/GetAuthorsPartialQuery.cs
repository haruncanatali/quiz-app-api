using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetAuthorsPartial;

public class GetAuthorsPartialQuery : IRequest<BaseResponseModel<List<CommonValue>>>
{
    
}