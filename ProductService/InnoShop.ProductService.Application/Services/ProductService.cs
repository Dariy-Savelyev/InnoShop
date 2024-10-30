using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Enums;
using InnoShop.ProductService.CrossCutting.Extensions;
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

    public async Task<IEnumerable<ProductSearchModel>> SearchProductsByNameAsync(string productName)
    {
        var productsDb = await productRepository.SearchProductsByNameAsync(productName);

        var products = mapper.Map<IEnumerable<ProductSearchModel>>(productsDb);

        return products;
    }

    public async Task<IEnumerable<ProductSearchModel>> SearchProductsBySubstringAsync(string productNameSubstring)
    {
        var productsDb = await productRepository.SearchProductsBySubstringAsync(productNameSubstring);

        var products = mapper.Map<IEnumerable<ProductSearchModel>>(productsDb);

        return products;
    }

    public async Task<IEnumerable<ProductSortingModel>> SortProductsByFieldAsync(SortFieldEnum sortField, SortOrderEnum sortOrder)
    {
        var productsDb = await productRepository.GetAllAsync();

        var products = mapper.Map<IEnumerable<ProductSortingModel>>(productsDb);

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

    public async Task CreateProductAsync(ProductCreationModel model, string userId)
    {
        var product = mapper.Map<Product>(model);
        product.UserId = userId;

        await productRepository.AddAsync(product);
    }

    public async Task EditProductAsync(ProductModificationModel model, string userId)
    {
        var productDb = await productRepository.GetByIdAsync(model.Id);

        if (productDb == null)
        {
            throw ExceptionHelper.GetNotFoundException("Product not found.");
        }

        if (productDb.UserId != userId)
        {
            throw ExceptionHelper.GetForbiddenException("This user can't interact with this product.");
        }

        productDb.Name = model.Name;

        productDb.Description = model.Description;

        productDb.Price = model.Price;

        productDb.IsAvailable = model.IsAvailable;

        await productRepository.ModifyAsync(productDb);
    }

    public async Task DeleteProductAsync(ProductDeletionModel model, string userId)
    {
        var productDb = await productRepository.GetByIdAsync(model.Id);

        if (productDb == null)
        {
            throw ExceptionHelper.GetNotFoundException("Product not found.");
        }

        if (productDb.UserId != userId)
        {
            throw ExceptionHelper.GetForbiddenException("This user can't interact with this product.");
        }

        await productRepository.DeleteAsync(productDb);
    }
}