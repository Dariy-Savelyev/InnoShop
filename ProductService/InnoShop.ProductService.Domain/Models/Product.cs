using InnoShop.ProductService.Domain.Models.Base;

namespace InnoShop.ProductService.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsAvailable { get; set; }
    public string UserId { get; set; }
}