using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class TicketMessageRepository :GenericRepository<TicketMessage, TicketMessageParameters>, ITicketMessageRepository
    {
        public TicketMessageRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<IReadOnlyList<TicketMessage>> GetAllByTicketIdAsync(int ticketId, TicketMessageParameters ticketMessageParameters)
        {
            IQueryable<TicketMessage> ticketMessages = _context.TicketMessages.Where(t => t.TicketId == ticketId);

            return await ticketMessages.ToListAsync();
        }

    }
}