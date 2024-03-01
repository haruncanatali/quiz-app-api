using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Literaries.Commands.Update;

public class UpdateLiteraryCommandValidator : AbstractValidator<UpdateLiteraryCommand>
{
    public UpdateLiteraryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.LiteraryId);
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.LiteraryName);
        RuleFor(c => c.Description).NotEmpty()
            .WithName(GlobalPropertyDisplayName.LiteraryDescription);
        RuleFor(c => c.AuthorId).NotNull()
            .WithName(GlobalPropertyDisplayName.AuthorId);
        RuleFor(c => c.PeriodId).NotNull()
            .WithName(GlobalPropertyDisplayName.PeriodId);
        RuleFor(c => c.LiteraryCategoryId).NotNull()
            .WithName(GlobalPropertyDisplayName.LiteraryCategoryId);
    }
}