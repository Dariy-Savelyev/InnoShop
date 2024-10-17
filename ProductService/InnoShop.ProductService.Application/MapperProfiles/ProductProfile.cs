using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Application.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreationProductModel, Product>()
            .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;

        CreateMap<Product, GetAllProductModel>();

        CreateMap<ModificationProductModel, Product>()
            .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;

        CreateMap<DeletionProductModel, Product>();

        CreateMap<Product, SearchProductModel>();
    }
}