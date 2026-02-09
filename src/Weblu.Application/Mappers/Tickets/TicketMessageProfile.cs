using AutoMapper;
using Weblu.Application.DTOs.Tickets.TicketMessageDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Mappers.Tickets
{
    public class TicketMessageProfile : Profile
    {
        public TicketMessageProfile()
        {
            CreateMap<TicketMessage, TicketMessageDTO>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<EditTicketMessageDTO, TicketMessage>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
            CreateMap<ReplyTicketDTO, TicketMessage>();
        }
    }
}