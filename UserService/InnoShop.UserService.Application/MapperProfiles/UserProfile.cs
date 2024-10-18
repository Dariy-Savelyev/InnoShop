using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        /*CreateMap<RegistrationModel, User>()
            .ForMember(
                dest => dest.EmailConfirmed,
                opt => opt.MapFrom(s => true))
            ;*/
    }
}