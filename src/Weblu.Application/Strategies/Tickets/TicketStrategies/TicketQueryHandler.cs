using Weblu.Application.Interfaces.Strategies.Tickets;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Strategies.Tickets.TicketStrategies
{
    public class TicketQueryHandler
    {
        private readonly ITicketQueryStrategy _ticketQueryStrategy;
        public TicketQueryHandler(ITicketQueryStrategy ticketQueryStrategy)
        {
            _ticketQueryStrategy = ticketQueryStrategy;
        }

        public IQueryable<Ticket> ExecuteTicketQuery(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            return _ticketQueryStrategy.Query(tickets, ticketParameters);
        }
    }
}