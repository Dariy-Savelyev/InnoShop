using FluentValidation.TestHelper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.Validators;

namespace InnoShop.UserService.Application.Tests.Validators
{
    public class UserResetPasswordValidatorTests
    {
        private readonly UserResetPasswordValidator _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenPasswordResetCodeTokenIsNullOrEmpty_ShouldHaveValidationError(string token)
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = token,
                Password = "ValidPass123",
                ConfirmPassword = "ValidPass123"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PasswordResetCodeToken);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("123456789")]
        public void Validate_WhenPasswordResetCodeTokenLengthIsInvalid_ShouldHaveValidationError(string token)
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = token,
                Password = "ValidPass123",
                ConfirmPassword = "ValidPass123"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PasswordResetCodeToken);
        }

        [Fact]
        public void Validate_WhenPasswordResetCodeTokenIsValid_ShouldNotHaveValidationError()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "ValidPass123",
                ConfirmPassword = "ValidPass123"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.PasswordResetCodeToken);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenPasswordIsNullOrEmpty_ShouldHaveValidationError(string password)
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = password,
                ConfirmPassword = password
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenPasswordIsTooShort_ShouldHaveValidationError()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "short",
                ConfirmPassword = "short"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenPasswordIsTooLong_ShouldHaveValidationError()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = new string('a', 256),
                ConfirmPassword = new string('a', 256)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenConfirmPasswordIsNullOrEmpty_ShouldHaveValidationError(string confirmPassword)
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "ValidPass123",
                ConfirmPassword = confirmPassword
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenConfirmPasswordIsTooShort_ShouldHaveValidationError()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "ValidPass123",
                ConfirmPassword = "short"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenConfirmPasswordIsTooLong_ShouldHaveValidationError()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "ValidPass123",
                ConfirmPassword = new string('a', 256)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenPasswordsDoNotMatch_ShouldHaveValidationError()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "ValidPass123",
                ConfirmPassword = "DifferentPass123"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenAllFieldsAreValid_ShouldNotHaveAnyValidationErrors()
        {
            var model = new PasswordResetModel
            {
                PasswordResetCodeToken = "12345678",
                Password = "ValidPass123",
                ConfirmPassword = "ValidPass123"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}