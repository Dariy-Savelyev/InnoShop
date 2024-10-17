using FluentValidation;
using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.Validators;

public class ProductDeletionValidator : AbstractValidator<DeletionProductModel>
{
    public ProductDeletionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}