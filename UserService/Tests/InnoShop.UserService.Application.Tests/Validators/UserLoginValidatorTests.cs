using FluentValidation.TestHelper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.Validators;

namespace InnoShop.UserService.Application.Tests.Validators
{
    public class UserLoginValidatorTests
    {
        private readonly UserLoginValidator _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenEmailIsNullOrEmpty_ShouldHaveValidationError(string email)
        {
            var model = new UserLoginModel { Email = email, Password = "validPassword" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_ShouldHaveValidationError()
        {
            var model = new UserLoginModel { Email = "invalid-email", Password = "validPassword" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationError()
        {
            var model = new UserLoginModel { Email = "valid@email.com", Password = "validPassword" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenPasswordIsNullOrEmpty_ShouldHaveValidationError(string password)
        {
            var model = new UserLoginModel { Email = "valid@email.com", Password = password };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenPasswordIsValid_ShouldNotHaveValidationError()
        {
            var model = new UserLoginModel { Email = "valid@email.com", Password = "validPassword" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            var model = new UserLoginModel { Email = "valid@email.com", Password = "validPassword" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}