using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Tickets;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Enums.Tickets.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class TicketRepository : GenericRepository<Ticket, TicketParameters>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IReadOnlyList<Ticket>> GetAllAsync(TicketParameters ticketParameters)
        {
            IQueryable<Ticket> tickets = _context.Tickets.AsNoTracking();
            if (ticketParameters.CreatedDateSort != CreatedDateSort.All)
            {
                tickets = new TicketQueryHandler(new CreatedDateSortStrategy())
                .ExecuteTicketQuery(tickets, ticketParameters);
            }

            if (ticketParameters.TicketStatus != TicketStatusSort.All)
            {
                tickets = new TicketQueryHandler(new StatusStrategy())
                .ExecuteTicketQuery(tickets, ticketParameters);
            }

            if (ticketParameters.TicketPriority != TicketPrioritySort.All)
            {
                tickets = new TicketQueryHandler(new PriorityStrategy())
                .ExecuteTicketQuery(tickets, ticketParameters);
            }

            return await tickets.ToListAsync();
        }

        public override async Task<Ticket?> GetByIdAsync(int ticketId)
        {
            Ticket? ticket = await _context.Tickets.Include(m => m.Messages).FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
            {
                return null;
            }
            return ticket;
        }
    }
}