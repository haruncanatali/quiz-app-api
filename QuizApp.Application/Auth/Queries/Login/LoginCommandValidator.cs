using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Auth.Queries.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithName(GlobalPropertyDisplayName.Password);
            RuleFor(x => x.Username).NotEmpty().WithName(GlobalPropertyDisplayName.UserName);
        }
    }
}
