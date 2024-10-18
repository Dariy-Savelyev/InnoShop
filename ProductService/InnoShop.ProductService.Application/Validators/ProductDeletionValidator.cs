using FluentValidation;
using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.Validators;

public class ProductDeletionValidator : AbstractValidator<ProductDeletionModel>
{
    public ProductDeletionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}