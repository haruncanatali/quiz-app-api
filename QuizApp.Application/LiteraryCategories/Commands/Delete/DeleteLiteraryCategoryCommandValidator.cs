using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.LiteraryCategories.Commands.Delete;

public class DeleteLiteraryCategoryCommandValidator : AbstractValidator<DeleteLiteraryCategoryCommand>
{
    public DeleteLiteraryCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}