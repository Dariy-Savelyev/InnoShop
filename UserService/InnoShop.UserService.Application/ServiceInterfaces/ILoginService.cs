using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface ILoginService : IBaseService
{
    Task<bool> LoginAsync(LoginModel model);
}