﻿using InnoShop.ProductService.Domain.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace InnoShop.ProductService.Domain.Models;

public class User : IdentityUser, IBaseDomainModel<string>
{
    public ICollection<Chat> CreatedChats { get; set; } = [];
    public ICollection<Chat> Chats { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}