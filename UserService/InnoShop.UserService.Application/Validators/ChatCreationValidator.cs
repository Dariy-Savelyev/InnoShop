using FluentValidation;
using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.Validators;

public class ChatCreationValidator : AbstractValidator<ChatModel>
{
    public ChatCreationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
    }
}