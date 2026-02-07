using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Domain.Entities.Tickets
{
    public class Ticket : BaseEntity
    {
        public string Subject { get; set; } = default!;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Normal;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public required string UserId { get; set; }
        public List<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
    }
}