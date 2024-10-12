using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain.Repositories;

public class MessageRepository(ApplicationContext context) : BaseRepository<Message, int>(context), IMessageRepository
{
    public async Task AddEmoteAsync(int messageId, bool? emoteValue)
    {
        var message = await Table.Where(x => x.Id == messageId).FirstOrDefaultAsync();

        message!.Emote = emoteValue;

        await SaveChangesAsync();
    }
}