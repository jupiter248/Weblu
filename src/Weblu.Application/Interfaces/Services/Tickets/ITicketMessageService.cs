using Weblu.Application.Dtos.Tickets.TicketMessageDtos;

namespace Weblu.Application.Interfaces.Services.Tickets
{
    public interface ITicketMessageService
    {
        Task<TicketMessageDto> GetByIdAsync(int ticketMessageId);
        Task<TicketMessageDto> ReplyAsync(string senderId, int ticketId, ReplyTicketDto replyTicketDto);
        Task<TicketMessageDto> EditAsync(string senderId, int messageId, EditTicketMessageDto editTicketMessageDto);
        Task DeleteAsync(string senderId, int messageId);
    }
}