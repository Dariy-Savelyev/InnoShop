using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductService.Domain.Repositories;

public class ProductRepository(ApplicationContext context) : BaseRepository<Product, int>(context), IProductRepository
{
    public async Task<Product?> SearchProductByNameAsync(string productName)
    {
        return await Table.SingleOrDefaultAsync(x => x.Name == productName);
    }

    public async Task<IEnumerable<Product?>> SearchProductsBySubstringAsync(string productNameSubstring)
    {
        return await Table.Where(x => x.Name.Contains(productNameSubstring)).ToListAsync();
    }
}