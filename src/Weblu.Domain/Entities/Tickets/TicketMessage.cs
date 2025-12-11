using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Tickets
{
    public class TicketMessage : BaseEntity
    {
        public string Message { get; set; } = default!;
        public bool IsFromAdmin { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public required string SenderId { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
    }
}