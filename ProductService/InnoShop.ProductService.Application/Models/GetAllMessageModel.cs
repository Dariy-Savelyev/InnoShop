﻿namespace InnoShop.ProductService.Application.Models;

public class GetAllMessageModel
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool? Emote { get; set; }
    public string UserId { get; set; } = string.Empty;
}