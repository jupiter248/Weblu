using Weblu.Application.Interfaces.Strategies.Tickets;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Tickets;
using Weblu.Domain.Enums.Tickets.Parameters;

namespace Weblu.Application.Strategies.Tickets.TicketStrategies
{
    public class StatusStrategy : ITicketQueryStrategy
    {
        public IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            if (ticketParameters.TicketStatus == TicketStatusSort.Open)
            {
                return tickets.Where(t => t.Status == TicketStatus.Open);
            }
            else if (ticketParameters.TicketStatus == TicketStatusSort.InProgress)
            {
                return tickets.Where(t => t.Status == TicketStatus.InProgress);
            }
            else if (ticketParameters.TicketStatus == TicketStatusSort.Closed)
            {
                return tickets.Where(t => t.Status == TicketStatus.Closed);
            }
            else
            {
                return tickets;
            }
        }
    }
}