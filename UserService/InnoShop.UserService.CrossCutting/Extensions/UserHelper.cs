using System.Security.Claims;

namespace InnoShop.UserService.CrossCutting.Extensions;

public static class UserHelper
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.GetValue(ClaimTypes.Name);
    }

    private static string GetValue(this ClaimsPrincipal user, string key)
    {
        return user.Claims.SingleOrDefault(x => x.Type == key)?.Value ?? string.Empty;
    }
}