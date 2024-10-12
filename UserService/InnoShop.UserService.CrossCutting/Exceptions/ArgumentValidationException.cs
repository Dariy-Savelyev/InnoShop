﻿using InnoShop.UserService.CrossCutting.Models;

namespace InnoShop.UserService.CrossCutting.Exceptions;

[Serializable]
public sealed class ArgumentValidationException : BaseException
{
    public ArgumentValidationException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Validation Failure. One or more validation errors occurred") => Errors = errors;
}