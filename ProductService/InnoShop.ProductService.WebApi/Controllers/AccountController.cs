using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.ProductService.WebApi.Controllers;

public class AccountController(IAccountService service) : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task Registration(RegistrationModel model)
    {
        await service.RegistrationAsync(model);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<string> Login(LoginModel model)
    {
        return await service.LoginAsync(model);
    }

    [HttpGet]
    public async Task<string> UserInfo()
    {
        return await Task.Run(() => User.GetUserId());
    }
}