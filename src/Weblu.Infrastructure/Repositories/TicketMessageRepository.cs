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
    public class TicketMessageRepository : ITicketMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTicketMessageAsync(TicketMessage message)
        {
            await _context.TicketMessages.AddAsync(message);
        }

        public void DeleteTicketMessage(TicketMessage message)
        {
            _context.TicketMessages.Remove(message);
        }

        public async Task<List<TicketMessage>> GetAllTicketMessagesAsync(int ticketId, TicketMessageParameters ticketMessageParameters)
        {
            IQueryable<TicketMessage> ticketMessages = _context.TicketMessages.Where(t => t.TicketId == ticketId).AsQueryable();

            return await ticketMessages.ToListAsync();
        }

        public async Task<TicketMessage?> GetTicketMessageByIdAsync(int messageId)
        {
            TicketMessage? ticketMessage = await _context.TicketMessages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (ticketMessage == null)
            {
                return null;
            }
            return ticketMessage;
        }

        public void UpdateTicketMessage(TicketMessage message)
        {
            _context.TicketMessages.Update(message);
        }
    }
}