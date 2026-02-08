using Weblu.Application.Dtos.Tickets.TicketDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Tickets;

namespace Weblu.Application.Interfaces.Services.Tickets
{
    public interface ITicketService
    {
        Task<List<TicketSummaryDto>> GetAllAsync(string userId, TicketParameters ticketParameters);
        Task<TicketDetailDto> GetByIdAsync(string userId, int ticketId);
        Task<TicketDetailDto> CreateAsync(string userId, CreateTicketDto createTicketDto);
        Task<TicketDetailDto> EditAsync(string userId, int ticketId, EditTicketDto editTicketDto);
        Task<TicketDetailDto> ChangeStatusAsync(int ticketId, ChangeTicketStatusDto changeTicketStatusDto);
        Task DeleteAsync(string userId, int ticketId);
    }
}