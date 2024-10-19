using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ServiceInterfaces;

public interface IAccountService : IBaseService
{
    Task<IEnumerable<GetAllUserModel>> GetAllUsersAsync();

    Task RegisterAsync(UserRegistrationModel model);

    Task EditUserAsync(UserModificationModel model);

    Task DeleteUserAsync(UserDeletionModel model);

    Task<string> LoginAsync(UserLoginModel model);
}