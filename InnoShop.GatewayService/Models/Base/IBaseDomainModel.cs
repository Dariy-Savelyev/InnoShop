using System.ComponentModel.DataAnnotations;

namespace InnoShop.GatewayService.Models.Base;

public interface IBaseDomainModel<TId>
{
    [Key]
    TId Id { get; set; }
}