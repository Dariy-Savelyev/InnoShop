using FluentValidation;
using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.Validators;

public class UserDeletionValidator : AbstractValidator<UserDeletionModel>
{
    public UserDeletionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}