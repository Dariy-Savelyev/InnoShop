using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Enums;
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

    public async Task<IEnumerable<SortedProductModel>> SortProductsByFieldAsync(SortFieldEnum sortField, SortOrderEnum sortOrder)
    {
        var productsDb = await productRepository.GetAllAsync();

        var products = mapper.Map<IEnumerable<SortedProductModel>>(productsDb);

        return sortField switch
        {
            SortFieldEnum.Name => sortOrder == SortOrderEnum.Descending
                ? products.OrderByDescending(x => x.Name)
                : products.OrderBy(x => x.Name),
            SortFieldEnum.Price => sortOrder == SortOrderEnum.Descending
                ? products.OrderByDescending(x => x.Price)
                : products.OrderBy(x => x.Price),
            _ => throw new ArgumentException("Invalid sort field", nameof(sortField))
        };
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