using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Commands.Create;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.AuthorName);
        RuleFor(c => c.Surname).NotEmpty()
            .WithName(GlobalPropertyDisplayName.AuthorSurname);
        RuleFor(c => c.Bio).NotEmpty()
            .WithName(GlobalPropertyDisplayName.AuthorBio);
        RuleFor(c => c.PeriodId).NotNull()
            .WithName(GlobalPropertyDisplayName.PeriodId);
        RuleFor(c => c.Photo).NotNull()
            .WithName(GlobalPropertyDisplayName.AuthorPhoto);
    }
}