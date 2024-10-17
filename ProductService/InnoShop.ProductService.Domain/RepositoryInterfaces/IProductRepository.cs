using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Domain.RepositoryInterfaces;

public interface IProductRepository : IBaseRepository<Product, int>
{
    bool IsUniqueName(string productName);
}