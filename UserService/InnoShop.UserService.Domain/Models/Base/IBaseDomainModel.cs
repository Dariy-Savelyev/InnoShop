using System.ComponentModel.DataAnnotations;

namespace InnoShop.UserService.Domain.Models.Base;

public interface IBaseDomainModel<TId>
{
    [Key]
    TId Id { get; set; }
}