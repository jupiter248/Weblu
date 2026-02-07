using Weblu.Application.Interfaces.Strategies.Tickets;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Tickets;
using Weblu.Domain.Enums.Tickets.Parameters;

namespace Weblu.Application.Strategies.Tickets.TicketStrategies
{
    public class PriorityStrategy : ITicketQueryStrategy
    {
        public IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            if (ticketParameters.TicketPriority == TicketPrioritySort.High)
            {
                return tickets.Where(t => t.Priority == TicketPriority.High);
            }
            else if (ticketParameters.TicketPriority == TicketPrioritySort.Normal)
            {
                return tickets.Where(t => t.Priority == TicketPriority.Normal);
            }
            else if (ticketParameters.TicketPriority == TicketPrioritySort.Low)
            {
                return tickets.Where(t => t.Priority == TicketPriority.Low);
            }
            else
            {
                return tickets;
            }
        }
    }
}