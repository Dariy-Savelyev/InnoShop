using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Application.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetAllProductModel>();

        CreateMap<DeletionProductModel, Product>();

        CreateMap<Product, SearchProductModel>();

        CreateMap<Product, SortedProductModel>();

        CreateMap<CreationProductModel, Product>()
            .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;

        CreateMap<ModificationProductModel, Product>()
            .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;
    }
}