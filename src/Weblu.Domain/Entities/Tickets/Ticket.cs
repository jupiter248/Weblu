using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Domain.Entities.Tickets
{
    public class Ticket : BaseEntity
    {
        // Required properties
        public string Subject { get; set; } = default!;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Normal;
        // Relationships
        public string UserId { get; set; } = default!;
        public List<TicketMessage> Messages { get; set; } = new ();
    }
}