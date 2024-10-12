using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface ILoginService : IBaseService
{
    Task<bool> LoginAsync(LoginModel model);
}