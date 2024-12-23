﻿namespace InnoShop.UserService.Domain.Models.Base;

public abstract class BaseEntity : IBaseDomainModel<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}