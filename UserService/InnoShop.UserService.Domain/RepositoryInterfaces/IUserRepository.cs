using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Domain.RepositoryInterfaces;

public interface IUserRepository : IBaseRepository<User, string>
{
    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByEmailConfirmationTokenAsync(string token);

    Task<User?> GetUserByVerificationCodeAsync(string verificationCode);

    bool IsUniqueEmail(string email);

    bool IsUniqueName(string userName);
}