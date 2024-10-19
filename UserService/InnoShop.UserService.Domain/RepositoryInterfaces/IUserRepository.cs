using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Domain.RepositoryInterfaces;

public interface IUserRepository : IBaseRepository<User, string>
{
    Task<User?> GetUserByEmailAsync(string email);
    bool IsUniqueEmail(string email);
    bool IsUniqueName(string userName);
}