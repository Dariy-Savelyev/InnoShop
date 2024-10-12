using InnoShop.UserService.Application.Models.Tokens;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.WebApi.Controllers;

public class TokensController(ITokensService service) : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Refresh(ValidateTokenModel request)
    {
        try
        {
            var userId = await service.ValidateAndGetUserIdTokenAsync(request.AccessToken);
            var result = await service.RefreshTokenAsync(userId);
            return new JsonResult(result);
        }
        catch (ArgumentException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            return Unauthorized("An error occurred while refreshing the token");
        }
    }

    [HttpPost]
    public async Task Revoke()
    {
        await service.RevokeTokenAsync(User.GetUserId());
    }
}