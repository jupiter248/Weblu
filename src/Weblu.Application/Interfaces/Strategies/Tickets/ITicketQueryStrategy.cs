using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Interfaces.Strategies.Tickets
{
    public interface ITicketQueryStrategy
    {
        IQueryable<Ticket> Query(IQueryable<Ticket> tickets, TicketParameters ticketParameters);
    }
}