using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.ProductService.WebApi.Controllers;

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