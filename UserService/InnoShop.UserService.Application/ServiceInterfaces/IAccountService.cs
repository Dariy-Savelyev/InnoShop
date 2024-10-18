using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface IAccountService : IBaseService
{
    Task RegisterAsync(UserRegistrationModel model);
    Task<bool> LoginAsync(UserLoginModel model);
}