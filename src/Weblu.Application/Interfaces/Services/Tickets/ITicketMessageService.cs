using Weblu.Application.Dtos.Tickets.TicketMessageDtos;

namespace Weblu.Application.Interfaces.Services.Tickets
{
    public interface ITicketMessageService
    {
        Task<TicketMessageDto> GetTicketMessageByIdAsync(int ticketMessageId);
        Task<TicketMessageDto> ReplyToTicketAsync(string senderId, int ticketId, ReplyTicketDto replyTicketDto);
        Task<TicketMessageDto> UpdateTicketMessageAsync(string senderId, int messageId, UpdateTicketMessageDto updateTicketMessageDto);
        Task DeleteTicketMessageAsync(string senderId, int messageId);
    }
}