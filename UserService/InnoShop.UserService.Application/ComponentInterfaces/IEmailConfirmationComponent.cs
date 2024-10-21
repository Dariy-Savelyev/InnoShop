using InnoShop.UserService.Application.Models;

namespace InnoShop.UserService.Application.ComponentInterfaces;

public interface IEmailConfirmationComponent : IBaseComponent
{
    Task SendEmailConfirmationLinkAsync(EmailConfirmationModel model);
}