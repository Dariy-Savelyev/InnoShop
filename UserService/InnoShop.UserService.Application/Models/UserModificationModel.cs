﻿namespace InnoShop.UserService.Application.Models;

public class UserModificationModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}