﻿namespace InnoShop.ProductService.Domain.Models.Base;

public abstract class BaseEntity : IBaseDomainModel<int>
{
    public int Id { get; set; }
}