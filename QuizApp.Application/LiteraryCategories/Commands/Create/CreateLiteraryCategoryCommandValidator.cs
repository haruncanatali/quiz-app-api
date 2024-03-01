using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.LiteraryCategories.Commands.Create;

public class CreateLiteraryCategoryCommandValidator : AbstractValidator<CreateLiteraryCategoryCommand>
{
    public CreateLiteraryCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.LiteraryCategoryName);
        RuleFor(c => c.Description).NotEmpty()
            .WithName(GlobalPropertyDisplayName.LiteraryCategoryDescription);
    }
}