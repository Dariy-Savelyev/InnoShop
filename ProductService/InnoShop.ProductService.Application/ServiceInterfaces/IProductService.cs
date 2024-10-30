using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.CrossCutting.Enums;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface IProductService : IBaseService
{
    Task<IEnumerable<GetAllProductModel>> GetAllProductsAsync();

    Task<IEnumerable<ProductSearchModel>> SearchProductsByNameAsync(string productName);

    Task<IEnumerable<ProductSearchModel>> SearchProductsBySubstringAsync(string productNameSubstring);

    Task<IEnumerable<ProductSortingModel>> SortProductsByFieldAsync(SortFieldEnum sortField, SortOrderEnum sortOrder);

    Task CreateProductAsync(ProductCreationModel model, string userId);

    Task EditProductAsync(ProductModificationModel model, string userId);

    Task DeleteProductAsync(ProductDeletionModel model, string userId);
}