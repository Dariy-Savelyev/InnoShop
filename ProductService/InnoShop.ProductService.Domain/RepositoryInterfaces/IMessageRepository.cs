using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Domain.RepositoryInterfaces;

public interface IMessageRepository : IBaseRepository<Message, int>
{
    Task AddEmoteAsync(int messageId, bool? emoteValue);
}