﻿namespace InnoShop.UserService.Application.Models;

public class GetAllChatModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<string> UserIds { get; set; } = [];
}