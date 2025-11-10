using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Errors.Tickets;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services
{
    public class TicketMessageService : ITicketMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public TicketMessageService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task DeleteTicketMessageAsync(string senderId, int messageId)
        {
            bool userExists = await _unitOfWork.Users.UserExistsAsync(senderId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isAdmin = await _userService.IsAdminAsync(senderId);
            TicketMessage ticketMessage = await _unitOfWork.TicketMessages.GetTicketMessageByIdAsync(messageId) ?? throw new NotFoundException(TicketMessageErrorCodes.TicketMessageNotFound);

            if (!isAdmin && ticketMessage.SenderId != senderId)
            {
                throw new UnauthorizedException(TicketErrorCodes.TicketDeleteForbidden);
            }

            _unitOfWork.TicketMessages.DeleteTicketMessage(ticketMessage);
            await _unitOfWork.CommitAsync();
        }

        public async Task<TicketMessageDto> GetTicketMessageByIdAsync(int ticketMessageId)
        {
            TicketMessage ticketMessage = await _unitOfWork.TicketMessages.GetTicketMessageByIdAsync(ticketMessageId) ?? throw new NotFoundException(TicketMessageErrorCodes.TicketMessageNotFound);
            TicketMessageDto ticketMessageDto = _mapper.Map<TicketMessageDto>(ticketMessage);
            return ticketMessageDto;
        }

        public async Task<TicketMessageDto> ReplyToTicketAsync(string senderId, int ticketId, ReplyTicketDto replyTicketDto)
        {
            bool userExists = await _unitOfWork.Users.UserExistsAsync(senderId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            bool isAdmin = await _userService.IsAdminAsync(senderId);
            Ticket ticket = await _unitOfWork.Tickets.GetTicketByIdAsync(ticketId) ?? throw new NotFoundException(TicketErrorCodes.TicketNotFound);
            if (ticket.UserId != senderId && !isAdmin)
            {
                throw new UnauthorizedException(TicketMessageErrorCodes.TicketMessageReplyForbidden);
            }
            TicketMessage ticketMessage = _mapper.Map<TicketMessage>(replyTicketDto);
            ticketMessage.IsFromAdmin = isAdmin;
            ticketMessage.Ticket = ticket;
            ticketMessage.TicketId = ticket.Id;
            ticketMessage.SenderId = senderId;


            await _unitOfWork.TicketMessages.AddTicketMessageAsync(ticketMessage);
            await _unitOfWork.CommitAsync();

            TicketMessageDto ticketMessageDto = _mapper.Map<TicketMessageDto>(ticketMessage);
            return ticketMessageDto;

        }

        public async Task<TicketMessageDto> UpdateTicketMessageAsync(string senderId, int messageId, UpdateTicketMessageDto updateTicketMessageDto)
        {
            bool userExists = await _unitOfWork.Users.UserExistsAsync(senderId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessage ticketMessage = await _unitOfWork.TicketMessages.GetTicketMessageByIdAsync(messageId) ?? throw new NotFoundException(TicketMessageErrorCodes.TicketMessageNotFound);

            if (ticketMessage.SenderId != senderId)
            {
                throw new UnauthorizedException(TicketMessageErrorCodes.TicketMessageUpdateForbidden);
            }
            ticketMessage = _mapper.Map(updateTicketMessageDto, ticketMessage);

            _unitOfWork.TicketMessages.UpdateTicketMessage(ticketMessage);
            await _unitOfWork.CommitAsync();

            TicketMessageDto ticketMessageDto = _mapper.Map<TicketMessageDto>(ticketMessage);
            return ticketMessageDto;
        }
    }
}