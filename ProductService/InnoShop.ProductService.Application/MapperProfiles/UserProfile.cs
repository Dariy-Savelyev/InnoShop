using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Application.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationModel, User>()
            .ForMember(
                dest => dest.EmailConfirmed,
                opt => opt.MapFrom(s => true))
            ;
    }
}