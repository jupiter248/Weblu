using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Tickets
{
    public class TicketMessage : BaseEntity
    {
        // Required properties
        public string Message { get; set; } = default!;
        public bool IsFromAdmin { get; set; }
        public int? ParentMessageId { get; set; }
        // Relationships
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
        public string SenderId { get; set; } = default!;
    }
}