using AutoMapper;
using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;

namespace InnoShop.UserService.Application.Services;

public class AccountService(
    IUserRepository userRepository,
    IMapper mapper,
    ITokenComponent tokenComponent,
    IConfiguration configuration,
    IEmailComponent emailComponent) : IAccountService
{
    public async Task<IEnumerable<GetAllUserModel>> GetAllUsersAsync()
    {
        var usersDb = await userRepository.GetAllAsync();

        var users = mapper.Map<IEnumerable<GetAllUserModel>>(usersDb);

        return users;
    }

    public async Task RegisterAsync(UserRegistrationModel model)
    {
        var user = mapper.Map<User>(model);
        user.EmailConfirmationToken = Guid.NewGuid().ToString();

        await userRepository.AddAsync(user);

        var emailModel = new EmailModel
        {
            ToAddress = user.Email,
            Subject = configuration["Email:EmailConfirmationSubject"]!,
            Body = $"{configuration["Email:LinkBodyMessage"]}{configuration["Email:BaseUrl"]}{user.EmailConfirmationToken}"
        };

        await emailComponent.SendEmailAsync(emailModel);
    }

    public async Task<string> LoginAsync(UserLoginModel model)
    {
        var user = await userRepository.GetUserByEmailAsync(model.Email);
        var passwordHash = PasswordHelper.HashPassword(model.Password);

        if (user == null || passwordHash != user.PasswordHash)
        {
            ExceptionHelper.ThrowForbiddenException("Incorrect credentials.");
        }

        return await tokenComponent.GetAccessTokenAsync(user!);
    }

    public async Task EditUserAsync(UserModificationModel model)
    {
        var userDb = await userRepository.GetByIdAsync(model.Id);

        if (userDb == null)
        {
            throw ExceptionHelper.GetNotFoundException("User not found.");
        }

        userDb.UserName = model.UserName;

        userDb.Email = model.Email;

        await userRepository.ModifyAsync(userDb);
    }

    public async Task DeleteUserAsync(UserDeletionModel model)
    {
        var userDb = await userRepository.GetByIdAsync(model.Id);

        if (userDb == null)
        {
            throw ExceptionHelper.GetNotFoundException("User not found.");
        }

        await userRepository.DeleteAsync(userDb);
    }

    public async Task ConfirmEmailAsync(string token)
    {
        var user = await userRepository.GetUserByEmailConfirmationTokenAsync(token);

        if (user == null)
        {
            throw ExceptionHelper.GetNotFoundException("Invalid token.");
        }

        if (user.IsEmailConfirmed)
        {
            throw ExceptionHelper.GetForbiddenException("Email already confirmed.");
        }

        user.IsEmailConfirmed = true;

        await userRepository.ModifyAsync(user);
    }

    public async Task SendEmailRecoveryPasswordAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            throw ExceptionHelper.GetNotFoundException("Wrong email.");
        }

        var recoveryCode = PasswordHelper.GeneratePasswordRecoveryCode();

        user.PasswordResetCodeToken = recoveryCode;
        await userRepository.ModifyAsync(user);

        var model = new EmailModel
        {
            ToAddress = user.Email,
            Subject = configuration["Email:PasswordRecoverySubject"]!,
            Body = $"{configuration["Email:PasswordBodyMessage"]}{recoveryCode}"
        };

        await emailComponent.SendEmailAsync(model);
    }

    public async Task VerifyPasswordRecoveryCodeAsync(string verificationCode)
    {
        var user = await userRepository.GetUserByVerificationCodeAsync(verificationCode);

        if (user == null)
        {
            throw ExceptionHelper.GetNotFoundException("Wrong verification code.");
        }
    }

    public async Task ResetPasswordAsync(PasswordResetModel model)
    {
        var user = await userRepository.GetUserByVerificationCodeAsync(model.PasswordResetCodeToken);

        if (user == null)
        {
            throw ExceptionHelper.GetNotFoundException("Invalid reset code.");
        }

        user!.PasswordHash = PasswordHelper.HashPassword(model.Password);
        user.PasswordResetCodeToken = string.Empty;

        await userRepository.ModifyAsync(user);
    }
}