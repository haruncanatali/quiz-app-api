using MediatR;
using QuizApp.Application.Authors.Queries.Dtos;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Queries.GetAuthors;

public class GetAuthorsQuery : IRequest<BaseResponseModel<List<AuthorDto>>>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public long? PeriodId { get; set; }
}