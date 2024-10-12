using InnoShop.UserService.Domain.Models.Base;

namespace InnoShop.UserService.Domain.Models;

public class Chat : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateCreate { get; set; }
    public string CreatorId { get; set; } = string.Empty;

    public User Creator { get; set; }
    public ICollection<User> Users { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}