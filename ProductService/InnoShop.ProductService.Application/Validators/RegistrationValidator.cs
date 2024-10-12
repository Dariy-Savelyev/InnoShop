using FluentValidation;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;

namespace InnoShop.ProductService.Application.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationModel>
{
    public RegistrationValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .Must(userRepository.IsUniqueName)
            .WithMessage("This name is taken. Please, enter a new name.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .Must(userRepository.IsUniqueEmail)
            .WithMessage("This email is taken. Please, enter a new email.");

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords aren't matching. Please, reenter password.");
    }
}