using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.ComponentInterfaces;

public interface ITokenComponent : IBaseComponent
{
    Task<string> GetAccessTokenAsync(User user);
}