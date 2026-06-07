using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Domain.Interfaces.Repositories.Tickets
{
    public interface ITicketMessageRepository : IGenericRepository<TicketMessage, TicketMessageParameters>
    {
        Task<IReadOnlyList<TicketMessage>> GetAllByTicketIdAsync(int ticketId, TicketMessageParameters ticketMessageParameters);
    }
}