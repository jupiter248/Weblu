using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ITicketMessageRepository
    {
        Task<IReadOnlyList<TicketMessage>> GetAllTicketMessagesAsync(int ticketId, TicketMessageParameters ticketMessageParameters);
        Task<TicketMessage?> GetTicketMessageByIdAsync(int messageId);
        Task AddTicketMessageAsync(TicketMessage message);
        void UpdateTicketMessage(TicketMessage message);
        void DeleteTicketMessage(TicketMessage message);
    }
}