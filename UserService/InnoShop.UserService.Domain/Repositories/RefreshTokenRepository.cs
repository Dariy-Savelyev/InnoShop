using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain.Repositories;

public class RefreshTokenRepository(ApplicationContext context) : BaseRepository<RefreshToken, int>(context), IRefreshTokenRepository
{
    public Task<RefreshToken?> GetActiveRefreshTokenAsync(string userId)
    {
        return Table.FirstOrDefaultAsync(t => t.UserId == userId && !t.RevokingDate.HasValue && DateTime.UtcNow < t.ExpirationDate);
    }

    public async Task RevokeAsync(string userId)
    {
        var tokens = await Table.Where(t => userId == t.UserId && !t.RevokingDate.HasValue).ToListAsync();
        tokens.ForEach(x => x.RevokingDate = DateTime.UtcNow);
        TableOriginal.UpdateRange(tokens);
        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
}