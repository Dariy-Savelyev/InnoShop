using FluentValidation;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;

namespace InnoShop.ProductService.Application.Validators;

public class ProductCreationValidator : AbstractValidator<CreationProductModel>
{
    public ProductCreationValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .Must(productRepository.IsUniqueName)
            .WithMessage("This name is taken. Please, enter a new name.");

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