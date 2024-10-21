namespace InnoShop.UserService.Application.Models;

public class PasswordResetModel
{
    public string PasswordResetCodeToken { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}