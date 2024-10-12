﻿using InnoShop.UserService.CrossCutting.Models;

namespace InnoShop.UserService.CrossCutting.Exceptions;

[Serializable]
public sealed class FailedDependencyException : BaseException
{
    public FailedDependencyException(IReadOnlyCollection<ResponseError> errors)
        : base(errors, "Failed Dependency. One or more validation errors occurred")
        => Errors = errors;
}