using AutoMapper;
using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.Services;
using InnoShop.UserService.CrossCutting.Exceptions;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace InnoShop.UserService.Application.Tests.Services;

public class AccountServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITokenComponent> _tokenComponentMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IEmailComponent> _emailComponentMock;
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _tokenComponentMock = new Mock<ITokenComponent>();
        _configurationMock = new Mock<IConfiguration>();
        _emailComponentMock = new Mock<IEmailComponent>();

        _accountService = new AccountService(
            _userRepositoryMock.Object,
            _mapperMock.Object,
            _tokenComponentMock.Object,
            _configurationMock.Object,
            _emailComponentMock.Object);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnMappedUsers()
    {
        // Arrange
        var usersDb = new List<User> { new() { Id = "1", Email = "test@test.com" } };
        var mappedUsers = new List<GetAllUserModel> { new() { Id = "1", Email = "test@test.com" } };

        _userRepositoryMock.Setup(x => x.GetAllAsync(false)).ReturnsAsync(usersDb);
        _mapperMock.Setup(x => x.Map<IEnumerable<GetAllUserModel>>(usersDb)).Returns(mappedUsers);

        // Act
        var result = await _accountService.GetAllUsersAsync();

        // Assert
        Assert.Equal(mappedUsers, result);
        _userRepositoryMock.Verify(x => x.GetAllAsync(false), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRegisterUserAndSendEmail()
    {
        // Arrange
        var registrationModel = new UserRegistrationModel { Email = "test@test.com" };
        var user = new User { Email = "test@test.com" };

        _mapperMock.Setup(x => x.Map<User>(registrationModel)).Returns(user);
        _configurationMock.Setup(x => x["Email:EmailConfirmationSubject"]).Returns("Confirm Email");
        _configurationMock.Setup(x => x["Email:LinkBodyMessage"]).Returns("Click here: ");
        _configurationMock.Setup(x => x["Email:BaseUrl"]).Returns("http://localhost/");

        // Act
        await _accountService.RegisterAsync(registrationModel);

        // Assert
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        _emailComponentMock.Verify(x => x.SendEmailAsync(It.IsAny<EmailModel>()), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ShouldReturnToken()
    {
        // Arrange
        var loginModel = new UserLoginModel { Email = "test@test.com", Password = "password" };
        var user = new User { Email = "test@test.com", PasswordHash = PasswordHelper.HashPassword("password") };
        var token = "token";

        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(loginModel.Email)).ReturnsAsync(user);
        _tokenComponentMock.Setup(x => x.GetAccessTokenAsync(user)).ReturnsAsync(token);

        // Act
        var result = await _accountService.LoginAsync(loginModel);

        // Assert
        Assert.Equal(token, result);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidCredentials_ShouldThrowException()
    {
        // Arrange
        var loginModel = new UserLoginModel { Email = "test@test.com", Password = "wrongpassword" };
        var user = new User { Email = "test@test.com", PasswordHash = PasswordHelper.HashPassword("password") };

        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(loginModel.Email)).ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<ForbiddenException>(() => _accountService.LoginAsync(loginModel));
    }

    [Fact]
    public async Task EditUserAsync_WithValidData_ShouldUpdateUser()
    {
        // Arrange
        var modificationModel = new UserModificationModel { Id = "1", UserName = "newname", Email = "new@email.com" };
        var user = new User { Id = "1" };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(modificationModel.Id)).ReturnsAsync(user);

        // Act
        await _accountService.EditUserAsync(modificationModel);

        // Assert
        Assert.Equal(modificationModel.UserName, user.UserName);
        Assert.Equal(modificationModel.Email, user.Email);
        _userRepositoryMock.Verify(x => x.ModifyAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_WithValidId_ShouldDeleteUser()
    {
        // Arrange
        var deletionModel = new UserDeletionModel { Id = "1" };
        var user = new User { Id = "1" };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(deletionModel.Id)).ReturnsAsync(user);

        // Act
        await _accountService.DeleteUserAsync(deletionModel);

        // Assert
        _userRepositoryMock.Verify(x => x.DeleteAsync(user), Times.Once);
    }

    [Fact]
    public async Task ConfirmEmailAsync_WithValidToken_ShouldConfirmEmail()
    {
        // Arrange
        var token = "valid-token";
        var user = new User { EmailConfirmationToken = token, IsEmailConfirmed = false };

        _userRepositoryMock.Setup(x => x.GetUserByEmailConfirmationTokenAsync(token)).ReturnsAsync(user);

        // Act
        await _accountService.ConfirmEmailAsync(token);

        // Assert
        Assert.True(user.IsEmailConfirmed);
        _userRepositoryMock.Verify(x => x.ModifyAsync(user), Times.Once);
    }

    [Fact]
    public async Task SendEmailRecoveryPasswordAsync_WithValidEmail_ShouldSendRecoveryEmail()
    {
        // Arrange
        var email = "test@test.com";
        var user = new User { Email = email };

        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(user);
        _configurationMock.Setup(x => x["Email:PasswordRecoverySubject"]).Returns("Reset Password");
        _configurationMock.Setup(x => x["Email:PasswordBodyMessage"]).Returns("Your code: ");

        // Act
        await _accountService.SendEmailRecoveryPasswordAsync(email);

        // Assert
        Assert.NotNull(user.PasswordResetCodeToken);
        _userRepositoryMock.Verify(x => x.ModifyAsync(user), Times.Once);
        _emailComponentMock.Verify(x => x.SendEmailAsync(It.IsAny<EmailModel>()), Times.Once);
    }

    [Fact]
    public async Task VerifyPasswordRecoveryCodeAsync_WithValidCode_ShouldNotThrowException()
    {
        // Arrange
        var verificationCode = "valid-code";
        var user = new User { PasswordResetCodeToken = verificationCode };

        _userRepositoryMock.Setup(x => x.GetUserByVerificationCodeAsync(verificationCode))
            .ReturnsAsync(user);

        // Act & Assert
        await _accountService.VerifyPasswordRecoveryCodeAsync(verificationCode); // Should not throw
        _userRepositoryMock.Verify(x => x.GetUserByVerificationCodeAsync(verificationCode), Times.Once);
    }

    [Fact]
    public async Task VerifyPasswordRecoveryCodeAsync_WithInvalidCode_ShouldThrowNotFoundException()
    {
        // Arrange
        var invalidCode = "invalid-code";

        _userRepositoryMock.Setup(x => x.GetUserByVerificationCodeAsync(invalidCode))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _accountService.VerifyPasswordRecoveryCodeAsync(invalidCode));
    }

    [Fact]
    public async Task ResetPasswordAsync_WithValidCode_ShouldUpdatePassword()
    {
        // Arrange
        var model = new PasswordResetModel
        {
            PasswordResetCodeToken = "valid-code",
            Password = "newPassword"
        };

        var user = new User
        {
            PasswordResetCodeToken = model.PasswordResetCodeToken,
            PasswordHash = "oldHash"
        };

        _userRepositoryMock.Setup(x => x.GetUserByVerificationCodeAsync(model.PasswordResetCodeToken))
            .ReturnsAsync(user);

        // Act
        await _accountService.ResetPasswordAsync(model);

        // Assert
        Assert.Equal(PasswordHelper.HashPassword(model.Password), user.PasswordHash);
        Assert.Empty(user.PasswordResetCodeToken);
        _userRepositoryMock.Verify(x => x.ModifyAsync(user), Times.Once);
    }

    [Fact]
    public async Task ResetPasswordAsync_WithInvalidCode_ShouldThrowNotFoundException()
    {
        // Arrange
        var model = new PasswordResetModel
        {
            PasswordResetCodeToken = "invalid-code",
            Password = "newPassword"
        };

        _userRepositoryMock.Setup(x => x.GetUserByVerificationCodeAsync(model.PasswordResetCodeToken))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _accountService.ResetPasswordAsync(model));
    }
}