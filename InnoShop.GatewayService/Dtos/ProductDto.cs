namespace InnoShop.GatewayService.Dtos;

public interface IProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsAvailable { get; set; }
    public string UserId { get; set; }
}