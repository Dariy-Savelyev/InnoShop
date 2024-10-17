using InnoShop.ProductService.Application.Models;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface IProductService : IBaseService
{
    Task<IEnumerable<GetAllProductModel>> GetAllProductsAsync();
    Task CreateProductAsync(CreationProductModel model);
    Task EditProductAsync(ModificationProductModel model);
    Task DeleteProductAsync(DeletionProductModel model);
}