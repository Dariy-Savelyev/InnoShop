using AutoMapper;
using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InnoShop.UserService.Application.Services;

public class AccountService(
    IUserRepository userRepository,
    IMapper mapper,
    ITokenComponent tokenComponent,
    IConfiguration configuration,
    IEmailConfirmationComponent emailConfirmationComponent) : IAccountService
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

        await SendEmailConfirmationAsync(user.Email, user.EmailConfirmationToken);
    }

    public async Task<string> LoginAsync(UserLoginModel model)
    {
        var user = await userRepository.GetUserByEmailAsync(model.Email);
        var passwordHash = PasswordHasher.HashPassword(model.Password);

        if (user == null || passwordHash != user.PasswordHash)
        {
            ExceptionHelper.ThrowUnauthorizedException("Incorrect credentials.");
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

    private async Task SendEmailConfirmationAsync(string email, string token)
    {
        var baseUrl = configuration["EmailConfirmationLink:BaseUrl"];
        var confirmationLink = $"{baseUrl}{token}";

        var model = new EmailConfirmationModel
        {
            ToAddress = email,
            Subject = configuration["EmailConfirmationLink:Subject"]!,
            Body = $"{configuration["EmailConfirmationLink:BodyMessage"]}{confirmationLink}"
        };

        await emailConfirmationComponent.SendEmailConfirmationLinkAsync(model);
    }
}