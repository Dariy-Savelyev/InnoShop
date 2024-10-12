using InnoShop.UserService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public sealed class ConflictException : BaseException
{
    public ConflictException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Conflict. One or more validation errors occurred") => Errors = errors;
}