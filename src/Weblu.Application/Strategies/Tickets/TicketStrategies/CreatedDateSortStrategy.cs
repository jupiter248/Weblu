using Weblu.Application.Interfaces.Strategies.Tickets;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Tickets.TicketStrategies
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