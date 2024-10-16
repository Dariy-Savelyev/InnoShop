using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.ProductService.WebApi.Controllers;

public class ProductController(IProductService service) : BaseController
{
    [HttpGet]
    public async Task<IEnumerable<GetAllProductModel>> GetAllProducts()
    {
        return await service.GetAllProductsAsync();
    }

    [HttpPost]
    public async Task Create(ProductModel model)
    {
        await service.CreateProductAsync(model);
    }

    [HttpPut]
    public async Task Edit(EditedProductModel model)
    {
        await service.EditProductAsync(model);
    }

    [HttpDelete]
    public async Task Delete(DeletedProductModel model)
    {
        await service.DeleteProductAsync(model);
    }
}