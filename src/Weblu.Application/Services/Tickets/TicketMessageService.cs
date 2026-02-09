using AutoMapper;
using Weblu.Application.DTOs.Tickets.TicketMessageDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Tickets;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Services.Tickets;
using Weblu.Application.Interfaces.Services.Users;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Errors.Tickets;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services.Tickets
{
    public class TicketMessageService : ITicketMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITicketMessageRepository _ticketMessageRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public TicketMessageService(
            IUnitOfWork unitOfWork, IMapper mapper, IUserService userService,
            ITicketMessageRepository ticketMessageRepository,
            ITicketRepository ticketRepository,
            IUserRepository userRepository
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _ticketMessageRepository = ticketMessageRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }
        public async Task DeleteAsync(string senderId, int messageId)
        {
            bool userExists = await _userRepository.UserExistsAsync(senderId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isAdmin = await _userService.IsAdminAsync(senderId);
            TicketMessage ticketMessage = await _ticketMessageRepository.GetByIdAsync(messageId) ?? throw new NotFoundException(TicketMessageErrorCodes.TicketMessageNotFound);

            if (!isAdmin && ticketMessage.SenderId != senderId)
            {
                throw new UnauthorizedException(TicketErrorCodes.TicketDeleteForbidden);
            }

            ticketMessage.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<TicketMessageDTO> GetByIdAsync(int ticketMessageId)
        {
            TicketMessage ticketMessage = await _ticketMessageRepository.GetByIdAsync(ticketMessageId) ?? throw new NotFoundException(TicketMessageErrorCodes.TicketMessageNotFound);
            TicketMessageDTO ticketMessageDTO = _mapper.Map<TicketMessageDTO>(ticketMessage);
            return ticketMessageDTO;
        }

        public async Task<TicketMessageDTO> ReplyAsync(string senderId, int ticketId, ReplyTicketDTO replyTicketDTO)
        {
            bool userExists = await _userRepository.UserExistsAsync(senderId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isAdmin = await _userService.IsAdminAsync(senderId);
            Ticket ticket = await _ticketRepository.GetByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            if (ticket.UserId != senderId && !isAdmin)
            {
                throw new UnauthorizedException(TicketMessageErrorCodes.TicketMessageReplyForbidden);
            }
            TicketMessage ticketMessage = _mapper.Map<TicketMessage>(replyTicketDTO);
            ticketMessage.IsFromAdmin = isAdmin;
            ticketMessage.Ticket = ticket;
            ticketMessage.TicketId = ticket.Id;
            ticketMessage.SenderId = senderId;


            _ticketMessageRepository.Add(ticketMessage);
            await _unitOfWork.CommitAsync();

            TicketMessageDTO ticketMessageDTO = _mapper.Map<TicketMessageDTO>(ticketMessage);
            return ticketMessageDTO;

        }

        public async Task<TicketMessageDTO> EditAsync(string senderId, int messageId, EditTicketMessageDTO editTicketMessageDTO)
        {
            bool userExists = await _userRepository.UserExistsAsync(senderId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessage ticketMessage = await _ticketMessageRepository.GetByIdAsync(messageId) ?? throw new NotFoundException(TicketMessageErrorCodes.TicketMessageNotFound);

            if (ticketMessage.SenderId != senderId)
            {
                throw new UnauthorizedException(TicketMessageErrorCodes.TicketMessageUpdateForbidden);
            }
            ticketMessage = _mapper.Map(editTicketMessageDTO, ticketMessage);

            ticketMessage.MarkUpdated();
            _ticketMessageRepository.Update(ticketMessage);
            await _unitOfWork.CommitAsync();

            TicketMessageDTO ticketMessageDTO = _mapper.Map<TicketMessageDTO>(ticketMessage);
            return ticketMessageDTO;
        }
    }
}