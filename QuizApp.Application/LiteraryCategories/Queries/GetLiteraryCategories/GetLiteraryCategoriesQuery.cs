using MediatR;
using QuizApp.Application.Common.Models;
using QuizApp.Application.LiteraryCategories.Queries.Dtos;

namespace QuizApp.Application.LiteraryCategories.Queries.GetLiteraryCategories;

public class GetLiteraryCategoriesQuery : IRequest<BaseResponseModel<List<LiteraryCategoryDto>>>
{
    public string? Name { get; set; }
}