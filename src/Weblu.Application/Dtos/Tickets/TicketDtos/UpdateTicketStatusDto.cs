using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.Tickets.TicketDtos
{
    public class UpdateTicketStatusDto
    {
        public TicketStatus Status { get; set; }
    }
}