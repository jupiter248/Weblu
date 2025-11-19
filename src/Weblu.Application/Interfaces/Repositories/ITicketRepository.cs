using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<IReadOnlyList<Ticket>> GetAllTicketsAsync(TicketParameters ticketParameters);
        Task<Ticket?> GetTicketByIdAsync(int ticketId);
        Task AddTicketAsync(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(Ticket ticket);
    }
}