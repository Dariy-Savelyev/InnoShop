namespace InnoShop.ProductService.Application.ComponentInterfaces;
public interface IBotComponent : IBaseComponent
{
    Task<string> GetResponseAsync(string prompt);
}