using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Domain.RepositoryInterfaces;

public interface IChatRepository : IBaseRepository<Chat, int>
{
    Task JoinUserAsync(string userId, int chatId);
}