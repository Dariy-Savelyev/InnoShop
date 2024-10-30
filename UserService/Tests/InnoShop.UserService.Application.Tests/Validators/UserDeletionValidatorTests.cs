using FluentValidation.TestHelper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.Validators;

namespace InnoShop.UserService.Application.Tests.Validators;

public class UserDeletionValidatorTests
{
    private readonly UserDeletionValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WhenIdIsNullOrEmpty_ShouldHaveValidationError(string id)
    {
        var model = new UserDeletionModel { Id = id };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WhenIdIsValid_ShouldNotHaveValidationError()
    {
        var model = new UserDeletionModel { Id = "validId" };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}