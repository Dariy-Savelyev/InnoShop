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
        var productsDb = await productRepository.GetAllAsync();

        var products = mapper.Map<IEnumerable<GetAllProductModel>>(productsDb);

        return products;
    }

    public async Task<SearchProductModel> SearchProductByNameAsync(string productName)
    {
        var productDb = await productRepository.SearchProductByNameAsync(productName);

        var product = mapper.Map<SearchProductModel>(productDb);

        return product;
    }

    public async Task<IEnumerable<SearchProductModel>> SearchProductsBySubstringAsync(string productNameSubstring)
    {
        var productsDb = await productRepository.SearchProductsBySubstringAsync(productNameSubstring);

        var products = mapper.Map<IEnumerable<SearchProductModel>>(productsDb);

        return products;
    }

    public async Task CreateProductAsync(CreationProductModel model)
    {
        var product = mapper.Map<Product>(model);

        await productRepository.AddAsync(product);
    }

    public async Task EditProductAsync(ModificationProductModel model)
    {
        var product = mapper.Map<Product>(model);

        await productRepository.ModifyAsync(product);
    }

    public async Task DeleteProductAsync(DeletionProductModel model)
    {
        var product = mapper.Map<Product>(model);

        await productRepository.DeleteAsync(product!);
    }
}