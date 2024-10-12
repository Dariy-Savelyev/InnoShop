using InnoShop.UserService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(IReadOnlyCollection<ResponseError> errors)
       : base(errors, "Unauthorized. One or more validation errors occurred")
       => Errors = errors;
}