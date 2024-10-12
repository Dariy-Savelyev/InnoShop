using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.WebApi.Controllers;

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