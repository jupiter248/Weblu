using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.DTOs.Tickets.TicketDTOs
{
    public class TicketSummaryDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; } = default!;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
    }
}