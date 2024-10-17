using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;

namespace InnoShop.ProductService.Domain.Repositories;

public class ProductRepository(ApplicationContext context) : BaseRepository<Product, int>(context), IProductRepository
{
    public bool IsUniqueName(string productName)
    {
        return Table.All(y => y.Name != productName);
    }
}