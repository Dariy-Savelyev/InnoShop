using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Domain.RepositoryInterfaces;

public interface IMessageRepository : IBaseRepository<Message, int>
{
    Task AddEmoteAsync(int messageId, bool? emoteValue);
}