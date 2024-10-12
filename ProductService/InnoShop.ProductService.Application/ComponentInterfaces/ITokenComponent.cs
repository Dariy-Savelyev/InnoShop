using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Application.ComponentInterfaces;

public interface ITokenComponent : IBaseComponent
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshToken GenerateRefreshToken(string appUserId);
    Task<string> RefreshTokenAsync(User user);
}