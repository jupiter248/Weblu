using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Parameters
{
    public class TicketParameters
    {
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public CreatedDateSort CreatedDateSort { get; set; }
    }
}