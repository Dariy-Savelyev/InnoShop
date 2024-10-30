using Microsoft.AspNetCore.Http;

namespace InnoShop.UserService.CrossCutting.Extensions;

public static class UserHelper
{
    public static string GetUserId(this HttpRequest request)
    {
        return request.Headers["User-Id"].FirstOrDefault() ?? string.Empty;
    }
}