using InnoShop.GatewayService.Models.Base;

namespace InnoShop.GatewayService.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsAvailable { get; set; }
    public string UserId { get; set; }

    public User User { get; set; }
}