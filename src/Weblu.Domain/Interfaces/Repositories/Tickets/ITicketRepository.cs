using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Domain.Interfaces.Repositories.Tickets
{
    public interface ITicketRepository : IGenericRepository<Ticket, TicketParameters>
    {
        Task<IReadOnlyList<Ticket>> GetAllByUserIdAsync(string userId, TicketParameters ticketParameters);
    }
}