using FluentValidation;
using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.Validators;

public class UserLoginValidator : AbstractValidator<UserLoginModel>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}