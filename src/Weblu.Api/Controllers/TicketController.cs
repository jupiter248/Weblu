using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketMessageService _ticketMessageService;
        public TicketController(ITicketService ticketService, ITicketMessageService ticketMessageService)
        {
            _ticketMessageService = ticketMessageService;
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTickets([FromQuery] TicketParameters ticketParameters)
        {
            List<TicketSummaryDto> ticketSummaryDtos = await _ticketService.GetAllTicketsAsync(ticketParameters);
            return Ok(ticketSummaryDtos);
        }
        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            TicketDetailDto ticketDetailDto = await _ticketService.GetTicketByIdAsync(ticketId);
            return Ok(ticketDetailDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] CreateTicketDto createTicketDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDto ticketDetailDto = await _ticketService.CreateTicketAsync(userId, createTicketDto);
            return CreatedAtAction(nameof(GetTicketById), new { ticketId = ticketDetailDto.Id }, ApiResponse<TicketDetailDto>.Success(
                "Ticket created successfully",
                ticketDetailDto
            ));
        }
        [HttpPut("{ticketId:int}")]
        public async Task<IActionResult> UpdateTicket(int ticketId, [FromBody] UpdateTicketDto updateTicketDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDto ticketDetailDto = await _ticketService.UpdateTicketAsync(userId, ticketId, updateTicketDto);
            return Ok(ApiResponse<TicketDetailDto>.Success(
                "Ticket updated successfully",
                ticketDetailDto
            ));
        }
        [HttpPut("{ticketId:int}/status")]
        public async Task<IActionResult> UpdateTicketStatus(int ticketId, [FromBody] UpdateTicketStatusDto updateTicketStatusDto)
        {
            TicketDetailDto ticketDetailDto = await _ticketService.UpdateTicketStatusAsync(ticketId, updateTicketStatusDto);
            return Ok(ApiResponse<TicketDetailDto>.Success(
                "Ticket status updated successfully",
                ticketDetailDto
            ));
        }
        [HttpDelete("{ticketId:int}")]
        public async Task<IActionResult> DeleteTicket(int ticketId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _ticketService.DeleteTicketAsync(userId, ticketId);
            return NoContent();
        }
        [HttpGet("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> GetTicketMessageById(int ticketMessageId)
        {
            TicketMessageDto ticketMessageDto = await _ticketMessageService.GetTicketMessageByIdAsync(ticketMessageId);
            return Ok(ticketMessageDto);
        }
        [HttpPost("{ticketId:int}/reply")]
        public async Task<IActionResult> ReplyTicket(int ticketId, [FromBody] ReplyTicketDto replyTicketDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessageDto ticketMessageDto = await _ticketMessageService.ReplyToTicketAsync(userId, ticketId, replyTicketDto);
            return CreatedAtAction(nameof(GetTicketMessageById), new { ticketMessageId = ticketMessageDto.Id }, ApiResponse<TicketMessageDto>.Success(
                "Ticket replied successfully",
                ticketMessageDto
            ));
        }
        [HttpPut("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> UpdateTicketMessage(int ticketMessageId, [FromBody] UpdateTicketMessageDto updateTicketMessageDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessageDto ticketMessageDto = await _ticketMessageService.UpdateTicketMessageAsync(userId, ticketMessageId, updateTicketMessageDto);
            return Ok(ApiResponse<TicketMessageDto>.Success(
                "Ticket message updated successfully",
                ticketMessageDto
            ));
        }
        [HttpDelete("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> DeleteTicketMessage(int ticketMessageId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _ticketMessageService.DeleteTicketMessageAsync(userId, ticketMessageId);
            return NoContent();
        }
    }
}