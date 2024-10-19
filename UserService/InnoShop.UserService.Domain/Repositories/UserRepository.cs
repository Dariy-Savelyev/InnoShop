using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain.Repositories;

public class UserRepository(ApplicationContext context) : BaseRepository<User, string>(context), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Table.SingleOrDefaultAsync(x => x.Email == email);
    }

    public bool IsUniqueEmail(string email)
    {
        return Table.All(x => x.Email != email);
    }

    public bool IsUniqueName(string userName)
    {
        return Table.All(y => y.UserName != userName);
    }
}