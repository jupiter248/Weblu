using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Tickets;

namespace Weblu.Application.Strategies.Tickets
{
    public class PriorityStrategy : ITicketQueryStrategy
    {
        public IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            if (ticketParameters.TicketPriority == TicketPriority.High)
            {
                return tickets.Where(t => t.Priority == TicketPriority.High);
            }
            else if (ticketParameters.TicketPriority == TicketPriority.Normal)
            {
                return tickets.Where(t => t.Priority == TicketPriority.Normal);
            }
            else if (ticketParameters.TicketPriority == TicketPriority.Low)
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