using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace InnoShop.UserService.Application.Components;

public class EmailConfirmationComponent(IConfiguration configuration) : IEmailConfirmationComponent
{
    public async Task SendEmailConfirmationLinkAsync(EmailConfirmationModel model)
    {
        var client = new SendGridClient(Environment.GetEnvironmentVariable("EmailApiKey"));
        var from = new EmailAddress(configuration["EmailConfirmationLink:Email"], configuration["EmailConfirmationLink:Name"]);
        var toAddress = new EmailAddress(model.ToAddress);

        var message = MailHelper.CreateSingleEmail(from, toAddress, model.Subject, null, model.Body);
        message.Subject = model.Subject;

        await client.SendEmailAsync(message);
    }
}