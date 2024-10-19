using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
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