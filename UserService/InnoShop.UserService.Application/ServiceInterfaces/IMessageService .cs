using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface IMessageService : IBaseService
{
    Task<int> SendMessageAsync(HubAddMessageModel model, string userId);
    Task<IEnumerable<GetAllMessageModel>> GetAllMessagesAsync(int chatId);
    Task SetEmoteAsync(MessageEmoteModel model);
}