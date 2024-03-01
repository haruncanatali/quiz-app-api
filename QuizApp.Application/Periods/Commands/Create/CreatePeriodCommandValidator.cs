using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Periods.Commands.Create;

public class CreatePeriodCommandValidator : AbstractValidator<CreatePeriodCommand>
{
    public CreatePeriodCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.PeriodName);
        RuleFor(c => c.Description).NotEmpty()
            .WithName(GlobalPropertyDisplayName.PeriodDescription);
    }
}