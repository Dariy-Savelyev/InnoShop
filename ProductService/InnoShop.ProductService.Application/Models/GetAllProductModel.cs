﻿namespace InnoShop.ProductService.Application.Models;

public class GetAllProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsAvailable { get; set; }
    public string UserId { get; set; }
}