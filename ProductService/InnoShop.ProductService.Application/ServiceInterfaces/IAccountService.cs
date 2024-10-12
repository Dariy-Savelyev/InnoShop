using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface IAccountService : IBaseService
{
    Task RegistrationAsync(RegistrationModel model);
    Task<string> LoginAsync(LoginModel model);
}