using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetGenders;

public class GetGendersQuery : IRequest<BaseResponseModel<List<CommonValue>>>
{
    
}