using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ITicketMessageRepository : IGenericRepository<TicketMessage, TicketMessageParameters>
    {
        Task<IReadOnlyList<TicketMessage>> GetAllByTicketIdAsync(int ticketId, TicketMessageParameters ticketMessageParameters);
    }
}