using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.ProductService.WebApi.Controllers;

public class ProductController(IProductService service) : BaseController
{
    [HttpGet]
    public async Task<IEnumerable<GetAllProductModel>> GetAllProducts()
    {
        return await service.GetAllProductsAsync();
    }

    [HttpGet]
    public async Task<ProductSearchModel> SearchProductByName(string productName)
    {
        return await service.SearchProductByNameAsync(productName);
    }

    [HttpGet]
    public async Task<IEnumerable<ProductSearchModel>> SearchProductsBySubstring(string productNameSubstring)
    {
        return await service.SearchProductsBySubstringAsync(productNameSubstring);
    }

    [HttpGet]
    public async Task<IEnumerable<ProductSortingModel>> SortProductsByField(
        SortFieldEnum sortField = SortFieldEnum.Name,
        SortOrderEnum sortOrder = SortOrderEnum.Ascending)
    {
        return await service.SortProductsByFieldAsync(sortField, sortOrder);
    }

    [HttpPost]
    public async Task Create(ProductCreationModel model)
    {
        await service.CreateProductAsync(model);
    }

    [HttpPut]
    public async Task Edit(ProductModificationModel model)
    {
        await service.EditProductAsync(model);
    }

    [HttpDelete]
    public async Task Delete(ProductDeletionModel model)
    {
        await service.DeleteProductAsync(model);
    }
}