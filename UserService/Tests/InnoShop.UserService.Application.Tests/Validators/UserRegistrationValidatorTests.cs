using FluentValidation.TestHelper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.Validators;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Moq;

namespace InnoShop.UserService.Application.Tests.Validators
{
    public class UserRegistrationValidatorTests
    {
        private readonly UserRegistrationValidator _validator;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserRegistrationValidatorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validator = new UserRegistrationValidator(_userRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenUserNameIsNullOrEmpty_ShouldHaveValidationError(string userName)
        {
            var model = new UserRegistrationModel { UserName = userName, Email = "valid@email.com", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Validate_WhenUserNameIsTooLong_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = new string('a', 256), Email = "valid@email.com", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Validate_WhenUserNameIsNotUnique_ShouldHaveValidationError()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueName("takenUser ")).Returns(false);
            var model = new UserRegistrationModel { UserName = "takenUser ", Email = "valid@email.com", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Validate_WhenUserNameIsUnique_ShouldNotHaveValidationError()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueName("uniqueUser ")).Returns(true);
            var model = new UserRegistrationModel { UserName = "uniqueUser ", Email = "valid@email.com", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenEmailIsNullOrEmpty_ShouldHaveValidationError(string email)
        {
            var model = new UserRegistrationModel { UserName = "validUser ", Email = email, Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = "validUser ", Email = "invalid-email", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsNotUnique_ShouldHaveValidationError()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueEmail("taken@email.com")).Returns(false);
            var model = new UserRegistrationModel { UserName = "validUser ", Email = "taken@email.com", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsUnique_ShouldNotHaveValidationError()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueEmail("unique@email.com")).Returns(true);
            var model = new UserRegistrationModel { UserName = "validUser ", Email = "unique@email.com", Password = "Password123", ConfirmPassword = "Password123" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenPasswordIsNullOrEmpty_ShouldHaveValidationError(string password)
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = password, ConfirmPassword = password };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenPasswordIsTooShort_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = "short", ConfirmPassword = "short" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenPasswordIsTooLong_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = new string('a', 256), ConfirmPassword = new string('a', 256) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Validate_WhenPasswordsDoNotMatch_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = "Password123", ConfirmPassword = "DifferentPassword123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenConfirmPasswordIsNullOrEmpty_ShouldHaveValidationError(string confirmPassword)
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = "Password123", ConfirmPassword = confirmPassword };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenConfirmPasswordIsTooShort_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = "Password123", ConfirmPassword = "short" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenConfirmPasswordIsTooLong_ShouldHaveValidationError()
        {
            var model = new UserRegistrationModel { UserName = "validUser", Email = "valid@email.com", Password = "Password123", ConfirmPassword = new string('a', 256) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Validate_WhenAllFieldsAreValid_ShouldNotHaveAnyValidationErrors()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueName("validUser")).Returns(true);
            _userRepositoryMock.Setup(repo => repo.IsUniqueEmail("valid@email.com")).Returns(true);

            var model = new UserRegistrationModel
            {
                UserName = "validUser",
                Email = "valid@email.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}