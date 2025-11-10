using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Enums.Tickets;
using Weblu.Domain.Enums.Tickets.Parameters;

namespace Weblu.Application.Parameters
{
    public class TicketParameters
    {
        public TicketStatusSort TicketStatus { get; set; }
        public TicketPrioritySort TicketPriority { get; set; }
        public CreatedDateSort CreatedDateSort { get; set; }
    }
}