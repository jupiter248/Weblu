using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;

namespace Weblu.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<List<TicketSummaryDto>> GetAllTicketsAsync(TicketParameters ticketParameters);
        Task<TicketDetailDto> GetTicketByIdAsync(int ticketId);
        Task<TicketDetailDto> CreateTicketAsync(string userId, CreateTicketDto createTicketDto);
        Task<TicketDetailDto> UpdateTicketAsync(string userId, int ticketId, UpdateTicketDto updateTicketDto);
        Task<TicketDetailDto> UpdateTicketStatusAsync(int ticketId, UpdateTicketStatusDto updateTicketStatusDto);
        Task DeleteTicketAsync(string userId, int ticketId);
    }
}