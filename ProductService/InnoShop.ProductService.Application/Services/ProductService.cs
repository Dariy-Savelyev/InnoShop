using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.Domain.Models;
using InnoShop.ProductService.Domain.RepositoryInterfaces;

namespace InnoShop.ProductService.Application.Services;

public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<GetAllProductModel>> GetAllProductsAsync()
    {
        var source = await productRepository.GetAllAsync();

        var products = mapper.Map<IEnumerable<GetAllProductModel>>(source);

        return products;
    }

    public async Task CreateProductAsync(ProductModel model)
    {
        var product = mapper.Map<Product>(model);

        await productRepository.AddAsync(product);
    }

    public async Task EditProductAsync(EditedProductModel model)
    {
        var product = mapper.Map<Product>(model);

        await productRepository.ModifyAsync(product);
    }

    public async Task DeleteProductAsync(DeletedProductModel model)
    {
        var product = mapper.Map<Product>(model);

        await productRepository.DeleteAsync(product!);
    }
}