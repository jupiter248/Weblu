using Weblu.Application.DTOs.Tickets.TicketMessageDTOs;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.DTOs.Tickets.TicketDTOs
{
    public class TicketDetailDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; } = default!;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public required string UserId { get; set; }
        public List<TicketMessageDTO>? Messages { get; set; }
    }
}