using Microsoft.AspNetCore.Http;

namespace InnoShop.UserService.CrossCutting.Extensions;

public static class UserHelper
{
    public static string GetUserId(this HttpRequest request)
    {
        return request.Headers["X-User-Id"].FirstOrDefault() ?? string.Empty;
    }
}