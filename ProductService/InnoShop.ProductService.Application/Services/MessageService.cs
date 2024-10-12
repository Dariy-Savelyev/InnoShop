using AutoMapper;
using InnoShop.ProductService.Application.ComponentInterfaces;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Exceptions;
using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;

namespace InnoShop.ProductService.Application.Services;

public class MessageService(
    IMessageRepository messageRepository,
    IChatRepository chatRepository,
    IBotComponent botComponent,
    IMapper mapper) : IMessageService
{
    public async Task<int> SendMessageAsync(HubAddMessageModel model, string userId)
    {
        var message = mapper.Map<Message>(model);
        message.UserId = userId;

        var chat = await chatRepository.GetByIdAsync(message.ChatId, y => y.Users);

        var user = chat!.Users.Any(x => x.Id == userId);

        if (!user)
        {
            throw new ForbiddenException(
                [
                    new(string.Empty, ["User is not authorized to send messages in this chat"])
                ]);
        }

        await messageRepository.AddAsync(message);

        var botMessage = await botComponent.GetResponseAsync(message.Content);

        if (botMessage != null)
        {
            message.Content = botMessage;

            await messageRepository.AddAsync(message);
        }

        Console.WriteLine("Response =======================> " + botMessage);

        return message.Id;
    }

    public async Task<IEnumerable<GetAllMessageModel>> GetAllMessagesAsync(int chatId)
    {
        var dataBaseMessages = await messageRepository.GetAllAsync();

        var chatMessages = dataBaseMessages.Where(x => x.ChatId == chatId);

        var messages = mapper.Map<IEnumerable<GetAllMessageModel>>(chatMessages);

        var listOfMessages = new List<GetAllMessageModel>();
        listOfMessages.AddRange(messages);

        return listOfMessages;
    }

    public async Task SetEmoteAsync(MessageEmoteModel model)
    {
        var emote = mapper.Map<Message>(model);

        await messageRepository.AddEmoteAsync(emote.Id, emote.Emote);
    }
}