using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.LiteraryCategories.Commands.Update;

public class UpdateLiteraryCategoryCommandValidator : AbstractValidator<UpdateLiteraryCategoryCommand>
{
    public UpdateLiteraryCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.LiteraryCategoryName);
        RuleFor(c => c.Description).NotEmpty()
            .WithName(GlobalPropertyDisplayName.LiteraryCategoryDescription);
    }
}