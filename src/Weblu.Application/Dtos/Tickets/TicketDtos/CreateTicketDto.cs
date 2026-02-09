using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.DTOs.Tickets.TicketDTOs
{
    public class CreateTicketDTO
    {
        public string Subject { get; set; } = default!;
        public TicketPriority Priority { get; set; }
        public string Message { get; set; } = default!;
    }
}