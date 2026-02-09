using Weblu.Application.DTOs.Tickets.TicketDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Tickets;

namespace Weblu.Application.Interfaces.Services.Tickets
{
    public interface ITicketService
    {
        Task<List<TicketSummaryDTO>> GetAllAsync(string userId, TicketParameters ticketParameters);
        Task<TicketDetailDTO> GetByIdAsync(string userId, int ticketId);
        Task<TicketDetailDTO> CreateAsync(string userId, CreateTicketDTO createTicketDTO);
        Task<TicketDetailDTO> EditAsync(string userId, int ticketId, EditTicketDTO editTicketDTO);
        Task<TicketDetailDTO> ChangeStatusAsync(int ticketId, ChangeTicketStatusDTO changeTicketStatusDTO);
        Task DeleteAsync(string userId, int ticketId);
    }
}