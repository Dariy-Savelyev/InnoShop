using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Domain.RepositoryInterfaces;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken, int>
{
    Task<RefreshToken?> GetActiveRefreshTokenAsync(string userId);
    Task RevokeAsync(string userId);
}