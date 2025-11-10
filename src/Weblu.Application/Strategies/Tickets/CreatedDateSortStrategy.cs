using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Tickets
{
    public class CreatedDateSortStrategy : ITicketQueryStrategy
    {
        public IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters)
        {
            if (ticketParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return tickets.OrderByDescending(t => t.CreatedAt);
            }
            else if (ticketParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return tickets.OrderBy(t => t.CreatedAt);
            }
            else
            {
                return tickets;
            }
        }
    }
}