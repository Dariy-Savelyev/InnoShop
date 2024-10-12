using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface IChatService : IBaseService
{
    Task<int> CreateChatAsync(ChatModel model, string userId);
    Task JoinChatAsync(JoinToChatModel model, string userId);
    Task<IEnumerable<GetAllChatModel>> GetAllChatsAsync();
}