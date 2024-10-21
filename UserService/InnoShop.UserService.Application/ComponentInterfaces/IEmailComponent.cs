using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ComponentInterfaces;

public interface IEmailComponent : IBaseComponent
{
    Task SendEmailAsync(EmailModel model);
}