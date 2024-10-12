using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;

namespace InnoShop.ProductService.Application.Services;

public class ChatService(IChatRepository chatRepository, IMapper mapper) : IChatService
{
    public async Task<int> CreateChatAsync(ChatModel model, string userId)
    {
        var chat = mapper.Map<Chat>(model);
        chat.CreatorId = userId;

        await chatRepository.AddAsync(chat);

        return chat.Id;
    }

    public async Task JoinChatAsync(JoinToChatModel model, string userId)
    {
        await chatRepository.JoinUserAsync(userId, model.ChatId);
    }

    public async Task<IEnumerable<GetAllChatModel>> GetAllChatsAsync()
    {
        var dataBaseChats = await chatRepository.GetAllAsync(y => y.Users);

        var source = dataBaseChats.OrderByDescending(x => x.DateCreate);
        var chats = mapper.Map<IEnumerable<GetAllChatModel>>(source);

        return chats;
    }
}