using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.CrossCutting.Enums;

namespace InnoShop.ProductService.Application.ServiceInterfaces;

public interface IProductService : IBaseService
{
    Task<IEnumerable<GetAllProductModel>> GetAllProductsAsync();

    Task<SearchProductModel> SearchProductByNameAsync(string productName);

    Task<IEnumerable<SearchProductModel>> SearchProductsBySubstringAsync(string productNameSubstring);

    Task<IEnumerable<SortedProductModel>> SortProductsByFieldAsync(SortFieldEnum sortField, SortOrderEnum sortOrder);

    Task CreateProductAsync(CreationProductModel model);

    Task EditProductAsync(ModificationProductModel model);

    Task DeleteProductAsync(DeletionProductModel model);
}