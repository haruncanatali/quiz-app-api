using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Commands.Delete;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}