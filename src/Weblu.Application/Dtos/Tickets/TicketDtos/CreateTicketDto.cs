using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.Tickets.TicketDtos
{
    public class CreateTicketDto
    {
        public string Subject { get; set; } = default!;
        public TicketPriority Priority { get; set; }
        public string Message { get; set; } = default!;
    }
}