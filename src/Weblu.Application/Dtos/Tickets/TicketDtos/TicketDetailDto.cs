using Weblu.Application.Dtos.Tickets.TicketMessageDtos;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.Tickets.TicketDtos
{
    public class TicketDetailDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = default!;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public required string UserId { get; set; }
        public List<TicketMessageDto>? Messages { get; set; }
    }
}