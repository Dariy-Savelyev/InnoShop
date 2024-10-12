using InnoShop.UserService.CrossCutting.Models;

namespace InnoShop.UserService.CrossCutting.Exceptions;

[Serializable]
public sealed class GoneException : BaseException
{
    public GoneException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Gone. One or more validation errors occurred")
        => Errors = errors;
}