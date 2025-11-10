using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Strategies.Tickets
{
    public class TicketQueryHandler
    {
        private readonly ITicketQueryStrategy _ticketQueryStrategy;
        public TicketQueryHandler(ITicketQueryStrategy ticketQueryStrategy)
        {
            _ticketQueryStrategy = ticketQueryStrategy;
        }

        public IQueryable<Ticket> ExecuteServiceQuery(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            return _ticketQueryStrategy.Query(tickets, ticketParameters);
        }
    }
}