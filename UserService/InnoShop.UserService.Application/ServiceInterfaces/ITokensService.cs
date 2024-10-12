namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface ITokensService : IBaseService
{
    Task<string> ValidateAndGetUserIdTokenAsync(string accessToken);
    Task<string> RefreshTokenAsync(string userId);
    Task RevokeTokenAsync(string userId);
}