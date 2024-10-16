using InnoShop.ProductService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public sealed class ForbiddenException : BaseException
{
    public ForbiddenException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Forbidden. One or more validation errors occurred")
        => Errors = errors;
}