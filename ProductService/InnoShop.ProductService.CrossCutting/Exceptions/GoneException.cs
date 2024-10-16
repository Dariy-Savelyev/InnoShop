﻿using InnoShop.ProductService.CrossCutting.Models;

namespace InnoShop.ProductService.CrossCutting.Exceptions;

[Serializable]
public sealed class GoneException : BaseException
{
    public GoneException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Gone. One or more validation errors occurred")
        => Errors = errors;
}