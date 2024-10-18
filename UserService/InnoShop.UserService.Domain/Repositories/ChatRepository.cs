using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain.Repositories;

public class ChatRepository(ApplicationContext context) : BaseRepository<Chat, int>(context), IChatRepository
{
    /*public async Task JoinUserAsync(string userId, int chatId)
    {
        var chat = await Table
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == chatId);

        var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        chat!.Users.Add(user!);

        await SaveChangesAsync();
    }*/
    public Task JoinUserAsync(string userId, int chatId)
    {
        throw new NotImplementedException();
    }
}