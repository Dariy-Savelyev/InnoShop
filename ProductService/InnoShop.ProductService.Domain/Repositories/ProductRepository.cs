using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductService.Domain.Repositories;

public class ProductRepository(ApplicationContext context) : BaseRepository<Product, int>(context), IProductRepository
{
    public Task<Product?> SearchProductByNameAsync(string productName)
    {
        return Table.SingleOrDefaultAsync(x => x.Name == productName);
    }

    public bool IsUniqueName(string productName)
    {
        return Table.All(y => y.Name != productName);
    }
}