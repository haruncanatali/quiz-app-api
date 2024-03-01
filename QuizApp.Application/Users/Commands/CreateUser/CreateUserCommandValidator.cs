using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithName(GlobalPropertyDisplayName.FirstName);
            RuleFor(x => x.Surname).NotEmpty().WithName(GlobalPropertyDisplayName.LastName);
            RuleFor(x => x.Email).EmailAddress().WithName(GlobalPropertyDisplayName.Email);
            RuleFor(x => x.Gender).NotNull().WithMessage("'Cinsiyet' alanı boş olmamalı.");
        }
    }
}