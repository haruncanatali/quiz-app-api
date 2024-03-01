﻿using FluentValidation;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithName(GlobalPropertyDisplayName.UpdateId);
        }
    }
}
