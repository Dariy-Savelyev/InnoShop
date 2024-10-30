using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Domain.RepositoryInterfaces;

public interface IProductRepository : IBaseRepository<Product, int>
{
    Task<IEnumerable<Product?>> SearchProductsByNameAsync(string productName);
    Task<IEnumerable<Product?>> SearchProductsBySubstringAsync(string productNameSubstring);
}