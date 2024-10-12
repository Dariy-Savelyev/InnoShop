﻿using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Domain.Models;

namespace InnoShop.UserService.Application.MapperProfiles;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<HubAddMessageModel, Message>()
            .ForMember(
                dest => dest.SendDate,
                opt => opt.MapFrom(x => DateTime.UtcNow))
            ;

        CreateMap<Message, GetAllMessageModel>();

        CreateMap<MessageEmoteModel, Message>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(x => x.MessageId))
            ;

        CreateMap<HubAddMessageModel, HubMessageModel>();
    }
}