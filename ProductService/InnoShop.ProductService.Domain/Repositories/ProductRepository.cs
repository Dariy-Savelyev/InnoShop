using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductService.Domain.Repositories;

public class ProductRepository(ApplicationContext context) : BaseRepository<Product, int>(context), IProductRepository
{
    public async Task<IEnumerable<Product?>> SearchProductsByNameAsync(string productName)
    {
        return await Table.Where(x => Equals(x.Name, productName)).ToListAsync();
    }

    public async Task<IEnumerable<Product?>> SearchProductsBySubstringAsync(string productNameSubstring)
    {
        return await Table.Where(x => x.Name.Contains(productNameSubstring)).ToListAsync();
    }
}