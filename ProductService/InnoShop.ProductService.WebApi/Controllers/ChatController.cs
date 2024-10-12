using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.ProductService.WebApi.Controllers;

public class ChatController(IChatService service) : BaseController
{
    [HttpPost]
    public async Task<int> Create(ChatModel model)
    {
        return await service.CreateChatAsync(model, User.GetUserId());
    }

    [HttpPost]
    public async Task Join(JoinToChatModel model)
    {
        await service.JoinChatAsync(model, User.GetUserId());
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllChatModel>> GetAllChats()
    {
        return await service.GetAllChatsAsync();
    }
}