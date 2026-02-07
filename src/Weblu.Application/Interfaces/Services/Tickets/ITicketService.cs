using Weblu.Application.Dtos.Tickets.TicketDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Tickets;

namespace Weblu.Application.Interfaces.Services.Tickets
{
    public interface ITicketService
    {
        Task<List<TicketSummaryDto>> GetAllTicketsAsync(string userId, TicketParameters ticketParameters);
        Task<TicketDetailDto> GetTicketByIdAsync(string userId, int ticketId);
        Task<TicketDetailDto> CreateTicketAsync(string userId, CreateTicketDto createTicketDto);
        Task<TicketDetailDto> UpdateTicketAsync(string userId, int ticketId, UpdateTicketDto updateTicketDto);
        Task<TicketDetailDto> UpdateTicketStatusAsync(int ticketId, UpdateTicketStatusDto updateTicketStatusDto);
        Task DeleteTicketAsync(string userId, int ticketId);
    }
}