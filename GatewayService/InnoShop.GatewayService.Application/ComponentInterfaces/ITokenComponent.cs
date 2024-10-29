using InnoShop.UserService.Domain.Models;

namespace InnoShop.GatewayService.Application.ComponentInterfaces;

public interface ITokenComponent : IBaseComponent
{
    Task<string> GetAccessTokenAsync(User user);
}