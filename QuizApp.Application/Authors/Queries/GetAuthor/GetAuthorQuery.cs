using MediatR;
using QuizApp.Application.Authors.Queries.Dtos;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Queries.GetAuthor;

public class GetAuthorQuery : IRequest<BaseResponseModel<AuthorDto>>
{
    public long Id { get; set; }
}