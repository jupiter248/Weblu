using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.Tickets.TicketDtos
{
    public class EditTicketDto
    {
        public string Subject { get; set; } = default!;
        public TicketPriority Priority { get; set; }
    }
}