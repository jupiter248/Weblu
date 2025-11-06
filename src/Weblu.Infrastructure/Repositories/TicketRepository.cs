using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsAsync(TicketParameters ticketParameters)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}