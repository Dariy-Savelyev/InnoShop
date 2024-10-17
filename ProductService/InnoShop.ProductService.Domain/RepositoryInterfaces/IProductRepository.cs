using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Domain.RepositoryInterfaces;

public interface IProductRepository : IBaseRepository<Product, int>
{
    Task<Product?> SearchProductByNameAsync(string productName);
    Task<IEnumerable<Product?>> SearchProductsBySubstringAsync(string productNameSubstring);
    bool IsUniqueName(string productName);
}