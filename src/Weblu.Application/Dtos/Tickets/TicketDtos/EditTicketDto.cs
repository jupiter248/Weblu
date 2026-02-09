using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.DTOs.Tickets.TicketDTOs
{
    public class EditTicketDTO
    {
        public string Subject { get; set; } = default!;
        public TicketPriority Priority { get; set; }
    }
}