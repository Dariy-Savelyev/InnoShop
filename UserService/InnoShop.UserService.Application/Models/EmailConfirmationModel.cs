namespace InnoShop.UserService.Application.Models;

public class EmailConfirmationModel
{
    public string ToAddress { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}