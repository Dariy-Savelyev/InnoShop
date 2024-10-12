using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductService.Domain.Repositories;

public class MessageRepository(ApplicationContext context) : BaseRepository<Message, int>(context), IMessageRepository
{
    public async Task AddEmoteAsync(int messageId, bool? emoteValue)
    {
        var message = await Table.Where(x => x.Id == messageId).FirstOrDefaultAsync();

        message!.Emote = emoteValue;

        await SaveChangesAsync();
    }
}