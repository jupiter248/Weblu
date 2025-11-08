using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Dtos.TicketDtos
{
    public class UpdateTicketDto
    {
        public string Subject { get; set; } = default!;
        public TicketPriority Priority { get; set; }
    }
}