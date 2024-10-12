using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface IAccountService : IBaseService
{
    Task RegistrationAsync(RegistrationModel model);
    Task<string> LoginAsync(LoginModel model);
}