using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Application.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetAllProductModel>();

        CreateMap<ProductDeletionModel, Product>();

        CreateMap<Product, ProductSearchModel>();

        CreateMap<Product, ProductSortingModel>();

        CreateMap<ProductCreationModel, Product>()
            .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;
    }
}