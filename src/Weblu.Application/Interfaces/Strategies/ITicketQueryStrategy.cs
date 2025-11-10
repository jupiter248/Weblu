using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface ITicketQueryStrategy
    {
        IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters);
    }
}