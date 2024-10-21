using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace InnoShop.UserService.Application.Components;

public class EmailComponent(IConfiguration configuration) : IEmailComponent
{
    public async Task SendEmailAsync(EmailModel model)
    {
        var client = new SendGridClient(configuration["Email:EmailApiKey"]);
        var from = new EmailAddress(configuration["Email:BaseEmail"], configuration["Email:Name"]);
        var toAddress = new EmailAddress(model.ToAddress);

        var message = MailHelper.CreateSingleEmail(from, toAddress, model.Subject, null, model.Body);
        message.Subject = model.Subject;

        await client.SendEmailAsync(message);
    }
}