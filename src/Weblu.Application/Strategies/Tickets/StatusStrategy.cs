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
    public class StatusStrategy : ITicketQueryStrategy
    {
        public IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            if (ticketParameters.TicketStatus == TicketStatus.Open)
            {
                return tickets.Where(t => t.Status == TicketStatus.Open);
            }
            else if (ticketParameters.TicketStatus == TicketStatus.InProgress)
            {
                return tickets.Where(t => t.Status == TicketStatus.InProgress);
            }
            else if (ticketParameters.TicketStatus == TicketStatus.Closed)
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