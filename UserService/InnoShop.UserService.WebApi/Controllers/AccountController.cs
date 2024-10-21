using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.WebApi.Controllers;

public class AccountController(IAccountService service) : BaseController
{
    [HttpGet]
    public async Task<IEnumerable<GetAllUserModel>> GetAllUsers()
    {
        return await service.GetAllUsersAsync();
    }

    [HttpGet]
    public async Task ConfirmEmail(string token)
    {
        await service.ConfirmEmailAsync(token);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task SendEmailRecoveryPassword(string email)
    {
        await service.SendEmailRecoveryPasswordAsync(email);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task VerifyPasswordRecoveryCode(string verificationCode)
    {
        await service.VerifyPasswordRecoveryCodeAsync(verificationCode);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task ResetPassword(PasswordResetModel model)
    {
        await service.ResetPasswordAsync(model);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task Register(UserRegistrationModel model)
    {
        await service.RegisterAsync(model);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<string> Login(UserLoginModel model)
    {
        return await service.LoginAsync(model);
    }

    [HttpPut]
    public async Task Edit(UserModificationModel model)
    {
        await service.EditUserAsync(model);
    }

    [HttpDelete]
    public async Task Delete(UserDeletionModel model)
    {
        await service.DeleteUserAsync(model);
    }
}