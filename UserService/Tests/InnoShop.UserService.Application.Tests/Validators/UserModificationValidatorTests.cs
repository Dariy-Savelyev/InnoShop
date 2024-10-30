using FluentValidation.TestHelper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.Validators;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Moq;

namespace InnoShop.UserService.Application.Tests.Validators
{
    public class UserModificationValidatorTests
    {
        private readonly UserModificationValidator _validator;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserModificationValidatorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validator = new UserModificationValidator(_userRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenIdIsNullOrEmpty_ShouldHaveValidationError(string id)
        {
            var model = new UserModificationModel { Id = id, UserName = "validUser ", Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenUserNameIsNullOrEmpty_ShouldHaveValidationError(string userName)
        {
            var model = new UserModificationModel { Id = "validId", UserName = userName, Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Validate_WhenUserNameIsTooLong_ShouldHaveValidationError()
        {
            var model = new UserModificationModel { Id = "validId", UserName = new string('a', 256), Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Validate_WhenUserNameIsNotUnique_ShouldHaveValidationError()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueName("takenUser ")).Returns(false);
            var model = new UserModificationModel { Id = "validId", UserName = "takenUser ", Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Validate_WhenUserNameIsUnique_ShouldNotHaveValidationError()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueName("uniqueUser ")).Returns(true);
            var model = new UserModificationModel { Id = "validId", UserName = "uniqueUser ", Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_WhenEmailIsNullOrEmpty_ShouldHaveValidationError(string email)
        {
            var model = new UserModificationModel { Id = "validId", UserName = "validUser ", Email = email };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_ShouldHaveValidationError()
        {
            var model = new UserModificationModel { Id = "validId", UserName = "validUser ", Email = "invalid-email" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationError()
        {
            var model = new UserModificationModel { Id = "validId", UserName = "validUser ", Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            _userRepositoryMock.Setup(repo => repo.IsUniqueName("uniqueUser ")).Returns(true);
            var model = new UserModificationModel { Id = "validId", UserName = "uniqueUser ", Email = "valid@email.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}