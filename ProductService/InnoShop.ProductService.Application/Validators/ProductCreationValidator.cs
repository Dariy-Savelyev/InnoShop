using FluentValidation;
using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.Validators;

public class ProductCreationValidator : AbstractValidator<ProductCreationModel>
{
    public ProductCreationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(18, 2, true)
            .WithMessage("Price must be a positive number with up to 2 decimal places.");

        RuleFor(x => x.IsAvailable)
            .NotNull();
    }
}