using Weblu.Application.Interfaces.Repositories.Tickets;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Entities.Tickets;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories.Tickets
{
    internal class TicketMessageRepository : GenericRepository<TicketMessage, TicketMessageParameters>, ITicketMessageRepository
    {
        public TicketMessageRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<IReadOnlyList<TicketMessage>> GetAllByTicketIdAsync(int ticketId, TicketMessageParameters ticketMessageParameters)
        {
            IQueryable<TicketMessage> ticketMessages = _context.TicketMessages.Where(a => !a.IsDeleted).Where(t => t.TicketId == ticketId);

            return await PaginationExtensions<TicketMessage>.GetPagedList(ticketMessages, ticketMessageParameters.PageNumber, ticketMessageParameters.PageSize);
        }

    }
}