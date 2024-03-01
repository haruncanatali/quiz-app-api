using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithName(GlobalPropertyDisplayName.UpdateId);
        }
    }
}
