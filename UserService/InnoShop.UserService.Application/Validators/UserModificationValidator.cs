using FluentValidation;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;

namespace InnoShop.UserService.Application.Validators;

public class UserModificationValidator : AbstractValidator<UserModificationModel>
{
    public UserModificationValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .Must(userRepository.IsUniqueName)
            .WithMessage("This name is taken. Please, enter a new name.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .EmailAddress();
    }
}