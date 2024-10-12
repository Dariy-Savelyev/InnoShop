using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.MapperProfiles;

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