using InnoShop.GatewayService.Dtos;
using InnoShop.ProductService.Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnoShop.ProductService.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsAvailable { get; set; }
    public string UserId { get; set; }

    [NotMapped]
    public IUserDto User { get; set; }
}