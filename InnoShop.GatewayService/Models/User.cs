using InnoShop.GatewayService.Models.Base;

namespace InnoShop.GatewayService.Models;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string EmailConfirmationToken { get; set; } = string.Empty;
    public string PasswordResetCodeToken { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}