using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface IMessageService : IBaseService
{
    Task<int> SendMessageAsync(HubAddMessageModel model, string userId);
    Task<IEnumerable<GetAllMessageModel>> GetAllMessagesAsync(int chatId);
    Task SetEmoteAsync(MessageEmoteModel model);
}