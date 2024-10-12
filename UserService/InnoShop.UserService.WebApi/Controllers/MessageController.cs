using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.WebApi.Controllers;

public class MessageController(IMessageService service) : BaseController
{
    [HttpPost]
    public async Task SetEmote(MessageEmoteModel model)
    {
        await service.SetEmoteAsync(model);
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllMessageModel>> GetAllMessages(int chatId)
    {
        return await service.GetAllMessagesAsync(chatId);
    }
}