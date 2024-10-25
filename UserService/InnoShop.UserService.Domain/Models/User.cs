﻿using InnoShop.GatewayService.Dtos;
using InnoShop.UserService.Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnoShop.UserService.Domain.Models;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string EmailConfirmationToken { get; set; } = string.Empty;
    public string PasswordResetCodeToken { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }

    [NotMapped]
    public ICollection<IProductDto> Products { get; set; }
}