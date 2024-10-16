using InnoShop.ProductService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public sealed class FailedDependencyException : BaseException
{
    public FailedDependencyException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Failed Dependency. One or more validation errors occurred")
        => Errors = errors;
}