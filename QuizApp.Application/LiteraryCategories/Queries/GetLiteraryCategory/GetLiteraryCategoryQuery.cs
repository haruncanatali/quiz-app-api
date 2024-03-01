using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.LiteraryCategories.Queries.Dtos;

namespace QuizApp.Application.LiteraryCategories.Queries.GetLiteraryCategory;

public class GetLiteraryCategoryQuery : IRequest<BaseResponseModel<LiteraryCategoryDto>>
{
    public long Id { get; set; }
}