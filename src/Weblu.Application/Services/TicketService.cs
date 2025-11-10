using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Errors.Tickets;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;
        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<TicketDetailDto> CreateTicketAsync(string userId, CreateTicketDto createTicketDto)
        {
            bool userExists = await _unitOfWork.Users.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            Ticket ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.UserId = userId;

            await _unitOfWork.Tickets.AddTicketAsync(ticket);

            TicketMessage ticketMessage = new TicketMessage()
            {
                SenderId = userId,
                Message = createTicketDto.Message,
                IsFromAdmin = false,
                Ticket = ticket,
                TicketId = ticket.Id
            };
            await _unitOfWork.TicketMessages.AddTicketMessageAsync(ticketMessage);
            await _unitOfWork.CommitAsync();

            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }

        public async Task DeleteTicketAsync(string userId, int ticketId)
        {
            bool userExists = await _unitOfWork.Users.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isAdmin = await _userService.IsAdminAsync(userId);
            Ticket ticket = await _unitOfWork.Tickets.GetTicketByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);

            if (!isAdmin && ticket.UserId != userId)
            {
                throw new UnauthorizedException(TicketErrorCodes.TicketDeleteForbidden);
            }

            _unitOfWork.Tickets.DeleteTicket(ticket);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TicketSummaryDto>> GetAllTicketsAsync(TicketParameters ticketParameters)
        {
            List<Ticket> tickets = await _unitOfWork.Tickets.GetAllTicketsAsync(ticketParameters);
            List<TicketSummaryDto> ticketSummaryDtos = _mapper.Map<List<TicketSummaryDto>>(tickets);

            return ticketSummaryDtos;
        }

        public async Task<TicketDetailDto> GetTicketByIdAsync(int ticketId)
        {
            Ticket ticket = await _unitOfWork.Tickets.GetTicketByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }

        public async Task<TicketDetailDto> UpdateTicketAsync(string userId, int ticketId, UpdateTicketDto updateTicketDto)
        {
            bool userExists = await _unitOfWork.Users.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            Ticket ticket = await _unitOfWork.Tickets.GetTicketByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            ticket = _mapper.Map(updateTicketDto, ticket);
            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }

        public async Task<TicketDetailDto> UpdateTicketStatusAsync(int ticketId, UpdateTicketStatusDto updateTicketStatusDto)
        {
            Ticket ticket = await _unitOfWork.Tickets.GetTicketByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            ticket = _mapper.Map(updateTicketStatusDto, ticket);
            _unitOfWork.Tickets.UpdateTicket(ticket);
            await _unitOfWork.CommitAsync();
            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }
    }
}