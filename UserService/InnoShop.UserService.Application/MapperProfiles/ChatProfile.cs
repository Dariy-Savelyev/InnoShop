using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.MapperProfiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<ChatModel, Chat>()
            .ForMember(
                dest => dest.DateCreate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;

        CreateMap<Chat, GetAllChatModel>()
            .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.Users.Select(u => u.Id)))
            ;
    }
}