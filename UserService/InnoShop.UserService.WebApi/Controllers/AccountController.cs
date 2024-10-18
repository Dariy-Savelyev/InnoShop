using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.WebApi.Controllers;

public class AccountController(IAccountService service) : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task Register(UserRegistrationModel model)
    {
        await service.RegisterAsync(model);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<bool> Login(UserLoginModel model)
    {
        return await service.LoginAsync(model);
    }
}