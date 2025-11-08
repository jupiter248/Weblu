using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Mappers
{
    public class TicketMessageProfile : Profile
    {
        public TicketMessageProfile()
        {
            CreateMap<TicketMessage, TicketMessageDto>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<UpdateTicketMessageDto, TicketMessage>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
            CreateMap<ReplyTicketDto, TicketMessage>();
        }
    }
}