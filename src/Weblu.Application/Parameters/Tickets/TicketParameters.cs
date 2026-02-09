using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Tickets.Parameters;

namespace Weblu.Application.Parameters.Tickets
{
    public class TicketParameters : BaseParameters
    {
        public TicketStatusSort TicketStatus { get; set; }
        public TicketPrioritySort TicketPriority { get; set; }
    }
}