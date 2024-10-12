using InnoShop.UserService.CrossCutting.Models;

namespace InnoShop.UserService.CrossCutting.Exceptions;

[Serializable]
public abstract class BaseException(IReadOnlyCollection<ResponseError> errors, string message)
    : ArgumentException(message)
{
    public IReadOnlyCollection<ResponseError> Errors { get; protected set; } = errors;
}