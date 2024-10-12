using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Domain.RepositoryInterfaces;

public interface IChatRepository : IBaseRepository<Chat, int>
{
    Task JoinUserAsync(string userId, int chatId);
}