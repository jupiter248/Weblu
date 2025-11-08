using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.TicketDtos
{
    public class TicketSummaryDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = default!;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
    }
}