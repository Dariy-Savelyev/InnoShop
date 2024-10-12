using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Domain.RepositoryInterfaces;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken, int>
{
    Task<RefreshToken?> GetActiveRefreshTokenAsync(string userId);
    Task RevokeAsync(string userId);
}