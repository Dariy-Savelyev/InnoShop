using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.ComponentInterfaces;

public interface ITokenComponent : IBaseComponent
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshToken GenerateRefreshToken(string appUserId);
    Task<string> RefreshTokenAsync(User user);
}