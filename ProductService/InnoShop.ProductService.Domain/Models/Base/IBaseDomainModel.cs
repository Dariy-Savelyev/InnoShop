using System.ComponentModel.DataAnnotations;

namespace InnoShop.ProductService.Domain.Models.Base;

public interface IBaseDomainModel<TId>
{
    [Key]
    TId Id { get; set; }
}