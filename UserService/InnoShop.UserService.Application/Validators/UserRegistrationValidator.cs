using FluentValidation;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;

namespace InnoShop.UserService.Application.Validators;

public class UserRegistrationValidator : AbstractValidator<RegistrationModel>
{
    public UserRegistrationValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .Must(userRepository.IsUniqueName)
            .WithMessage("This name is taken.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .EmailAddress()
            .Must(userRepository.IsUniqueEmail)
            .WithMessage("This email is already registered.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .MinimumLength(8)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords aren't matching.");
    }
}