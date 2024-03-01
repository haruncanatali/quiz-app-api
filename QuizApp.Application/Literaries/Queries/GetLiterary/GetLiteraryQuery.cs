using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;

namespace QuizApp.Application.Literaries.Queries.GetLiterary;

public class GetLiteraryQuery : IRequest<BaseResponseModel<LiteraryDto>>
{
    public long Id { get; set; }
}