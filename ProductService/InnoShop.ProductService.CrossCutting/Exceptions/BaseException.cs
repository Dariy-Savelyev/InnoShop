using InnoShop.ProductService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public abstract class BaseException(IReadOnlyCollection<ResponseError> errors, string message)
    : ArgumentException(message)
{
    public IReadOnlyCollection<ResponseError> Errors { get; protected set; } = errors;
}