using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.TicketDtos
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