using FluentValidation;
using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.Validators;

public class UserResetPasswordValidator : AbstractValidator<PasswordResetModel>
{
    public UserResetPasswordValidator()
    {
        RuleFor(x => x.PasswordResetCodeToken)
            .NotEmpty()
            .NotNull()
            .MaximumLength(8)
            .MinimumLength(8);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .MinimumLength(8)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords aren't matching.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .MinimumLength(8)
            .Equal(x => x.Password)
            .WithMessage("Passwords aren't matching.");
    }
}