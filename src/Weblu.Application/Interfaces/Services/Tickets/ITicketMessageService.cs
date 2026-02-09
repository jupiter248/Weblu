using Weblu.Application.DTOs.Tickets.TicketMessageDTOs;

namespace Weblu.Application.Interfaces.Services.Tickets
{
    public interface ITicketMessageService
    {
        Task<TicketMessageDTO> GetByIdAsync(int ticketMessageId);
        Task<TicketMessageDTO> ReplyAsync(string senderId, int ticketId, ReplyTicketDTO replyTicketDTO);
        Task<TicketMessageDTO> EditAsync(string senderId, int messageId, EditTicketMessageDTO editTicketMessageDTO);
        Task DeleteAsync(string senderId, int messageId);
    }
}