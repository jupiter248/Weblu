using AutoMapper;
using Weblu.Application.Dtos.Tickets.TicketDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Mappers.Tickets
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketSummaryDto>();
            CreateMap<Ticket, TicketDetailDto>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<EditTicketDto, Ticket>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
            CreateMap<ChangeTicketStatusDto, Ticket>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
            CreateMap<CreateTicketDto, Ticket>();
        }
    }
}