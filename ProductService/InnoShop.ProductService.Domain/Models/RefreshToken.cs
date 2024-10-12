﻿using InnoShop.ProductService.Domain.Models.Base;

namespace InnoShop.ProductService.Domain.Models;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? RevokingDate { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}