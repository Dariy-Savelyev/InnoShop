namespace InnoShop.UserService.Application.ComponentInterfaces;
public interface IBotComponent : IBaseComponent
{
    Task<string> GetResponseAsync(string prompt);
}