using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Periods.Commands.Update;

public class UpdatePeriodCommandValidator : AbstractValidator<UpdatePeriodCommand>
{
    public UpdatePeriodCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
        RuleFor(c => c.Description).NotEmpty()
            .WithName(GlobalPropertyDisplayName.PeriodDescription);
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.PeriodName);
    }
}