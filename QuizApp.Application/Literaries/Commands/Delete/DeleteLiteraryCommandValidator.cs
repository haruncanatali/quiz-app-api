using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Literaries.Commands.Delete;

public class DeleteLiteraryCommandValidator : AbstractValidator<DeleteLiteraryCommand>
{
    public DeleteLiteraryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.LiteraryId);
    }
}