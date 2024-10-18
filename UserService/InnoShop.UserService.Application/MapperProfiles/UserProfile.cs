using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.CrossCutting.Constants;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationModel, User>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt
                    .MapFrom(s => PasswordHasher.HashPassword(s.Password)))
            .ForMember(
                dest => dest.Role,
                opt => opt
                    .MapFrom(x => AccountConstants.Role))
            ;

        CreateMap<UserLoginModel, User>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(s => PasswordHasher.HashPassword(s.Password)))
            ;
    }
}