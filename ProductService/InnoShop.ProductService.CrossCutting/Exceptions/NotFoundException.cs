using InnoShop.ProductService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public sealed class NotFoundException : BaseException
{
    public NotFoundException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Not Found. One or more validation errors occurred")
        => Errors = errors;
}