using FluentValidation;
using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.Validators;

public class LoginValidator : AbstractValidator<LoginModel>
{
    public LoginValidator()
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