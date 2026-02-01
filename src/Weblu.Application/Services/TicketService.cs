using AutoMapper;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users;
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
        private readonly ITicketMessageRepository _ticketMessageRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public TicketService(IUnitOfWork unitOfWork, IMapper mapper,
             ITicketRepository ticketRepository,
             ITicketMessageRepository ticketMessageRepository,
             IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _ticketMessageRepository = ticketMessageRepository;
        }
        public async Task<TicketDetailDto> CreateTicketAsync(string userId, CreateTicketDto createTicketDto)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            Ticket ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.UserId = userId;

            _ticketRepository.Add(ticket);

            TicketMessage ticketMessage = new TicketMessage()
            {
                SenderId = userId,
                Message = createTicketDto.Message,
                IsFromAdmin = false,
                Ticket = ticket,
                TicketId = ticket.Id
            };
            _ticketMessageRepository.Add(ticketMessage);
            await _unitOfWork.CommitAsync();

            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }

        public async Task DeleteTicketAsync(string userId, int ticketId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isAdmin = await _userRepository.IsAdminAsync(userId);
            Ticket ticket = await _ticketRepository.GetByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);

            if (!isAdmin && ticket.UserId != userId)
            {
                throw new UnauthorizedException(TicketErrorCodes.TicketDeleteForbidden);
            }

            _ticketRepository.Delete(ticket);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TicketSummaryDto>> GetAllTicketsAsync(string userId, TicketParameters ticketParameters)
        {
            var isAdmin = await _userRepository.IsAdminAsync(userId);
            IReadOnlyList<Ticket> tickets;
            if (!isAdmin)
            {
                tickets = await _ticketRepository.GetAllByUserIdAsync(userId, ticketParameters);
            }
            else
            {
                tickets = await _ticketRepository.GetAllAsync(ticketParameters);
            }
            List<TicketSummaryDto> ticketSummaryDtos = _mapper.Map<List<TicketSummaryDto>>(tickets);

            return ticketSummaryDtos;
        }

        public async Task<TicketDetailDto> GetTicketByIdAsync(string userId, int ticketId)
        {
            var isAdmin = await _userRepository.IsAdminAsync(userId);
            Ticket ticket = await _ticketRepository.GetByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            if (!isAdmin && userId != ticket.UserId)
            {
                throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            }
            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }

        public async Task<TicketDetailDto> UpdateTicketAsync(string userId, int ticketId, UpdateTicketDto updateTicketDto)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            Ticket ticket = await _ticketRepository.GetByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            if (ticket.UserId != userId)
            {
                throw new UnauthorizedException(TicketErrorCodes.TicketUpdateForbidden);
            }
            ticket = _mapper.Map(updateTicketDto, ticket);
            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }

        public async Task<TicketDetailDto> UpdateTicketStatusAsync(int ticketId, UpdateTicketStatusDto updateTicketStatusDto)
        {
            Ticket ticket = await _ticketRepository.GetByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            ticket = _mapper.Map(updateTicketStatusDto, ticket);
            _ticketRepository.Update(ticket);
            await _unitOfWork.CommitAsync();
            TicketDetailDto ticketDetailDto = _mapper.Map<TicketDetailDto>(ticket);
            return ticketDetailDto;
        }
    }
}