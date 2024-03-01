using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;

namespace QuizApp.Application.Literaries.Queries.GetLiteraries;

public class GetLiterariesQuery : IRequest<BaseResponseModel<List<LiteraryDto>>>
{
    public string? Name { get; set; }
}