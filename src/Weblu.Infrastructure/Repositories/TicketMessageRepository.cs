using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Tickets;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class TicketMessageRepository : ITicketMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddTicketMessageAsync(TicketMessage message)
        {
            throw new NotImplementedException();
        }

        public void DeleteTicketMessage(TicketMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<TicketMessage> GetTicketMessageByIdAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTicketMessage(TicketMessage message)
        {
            throw new NotImplementedException();
        }
    }
}