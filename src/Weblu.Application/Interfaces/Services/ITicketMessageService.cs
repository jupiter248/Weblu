using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.TicketDtos;

namespace Weblu.Application.Interfaces.Services
{
    public interface ITicketMessageService
    {
        Task<TicketMessageDto> GetTicketMessageByIdAsync(int ticketMessageId);
        Task<TicketMessageDto> ReplyToTicketAsync(string senderId, int ticketId, ReplyTicketDto replyTicketDto);
        Task<TicketMessageDto> UpdateTicketMessageAsync(string senderId, int messageId, UpdateTicketMessageDto updateTicketMessageDto);
        Task DeleteTicketMessageAsync(string senderId, int messageId);
    }
}