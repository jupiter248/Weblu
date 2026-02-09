using AutoMapper;
using Weblu.Application.DTOs.Tickets.TicketDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Mappers.Tickets
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketSummaryDTO>();
            CreateMap<Ticket, TicketDetailDTO>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<EditTicketDTO, Ticket>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
            CreateMap<ChangeTicketStatusDTO, Ticket>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
            CreateMap<CreateTicketDTO, Ticket>();
        }
    }
}