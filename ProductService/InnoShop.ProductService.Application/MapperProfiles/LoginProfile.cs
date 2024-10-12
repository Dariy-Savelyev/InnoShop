using AutoMapper;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.CrossCutting.Extensions;
using InnoShop.ProductService.Domain.Models;

namespace InnoShop.ProductService.Application.MapperProfiles;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginModel, User>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(s => PasswordHasher.HashPassword(s.Password)))
            ;
    }
}