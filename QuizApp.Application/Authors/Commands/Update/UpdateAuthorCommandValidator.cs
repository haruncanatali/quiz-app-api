using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Commands.Update;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.AuthorId);
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.AuthorName);
        RuleFor(c => c.Surname).NotEmpty()
            .WithName(GlobalPropertyDisplayName.AuthorSurname);
        RuleFor(c => c.Bio).NotEmpty()
            .WithName(GlobalPropertyDisplayName.AuthorBio);
        RuleFor(c => c.PeriodId).NotNull()
            .WithName(GlobalPropertyDisplayName.PeriodId);
    }
}