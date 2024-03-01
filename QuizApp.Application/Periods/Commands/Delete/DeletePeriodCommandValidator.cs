using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Periods.Commands.Delete;

public class DeletePeriodCommandValidator : AbstractValidator<DeletePeriodCommand>
{
    public DeletePeriodCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}