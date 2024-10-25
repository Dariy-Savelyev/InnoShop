namespace InnoShop.GatewayService.Dtos;

public interface IUserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
    public string EmailConfirmationToken { get; set; }
    public string PasswordResetCodeToken { get; set; }
    public bool IsEmailConfirmed { get; set; }
}