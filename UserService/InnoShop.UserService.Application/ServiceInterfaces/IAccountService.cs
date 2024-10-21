using InnoShop.UserService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface IAccountService : IBaseService
{
    Task<IEnumerable<GetAllUserModel>> GetAllUsersAsync();

    Task RegisterAsync(UserRegistrationModel model);

    Task EditUserAsync(UserModificationModel model);

    Task DeleteUserAsync(UserDeletionModel model);

    Task<string> LoginAsync(UserLoginModel model);

    Task ConfirmEmailAsync(string token);

    Task SendEmailRecoveryPasswordAsync(string email);

    Task VerifyPasswordRecoveryCodeAsync(string verificationCode);

    Task ResetPasswordAsync(PasswordResetModel model);
}