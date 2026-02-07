using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Tickets.TicketDtos;
using Weblu.Application.Dtos.Tickets.TicketMessageDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Tickets;
using Weblu.Application.Parameters.Tickets;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Tickets
{
    [ApiVersion("1")]
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTickets([FromQuery] TicketParameters ticketParameters)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<TicketSummaryDto> ticketSummaryDtos = await _ticketService.GetAllTicketsAsync(userId, ticketParameters);
            return Ok(ticketSummaryDtos);
        }
        [Authorize]
        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDto ticketDetailDto = await _ticketService.GetTicketByIdAsync(userId, ticketId);
            return Ok(ticketDetailDto);
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize(Policy = Permissions.ManageTickets)]
        [HttpPut("{ticketId:int}/status")]
        public async Task<IActionResult> UpdateTicketStatus(int ticketId, [FromBody] UpdateTicketStatusDto updateTicketStatusDto)
        {
            TicketDetailDto ticketDetailDto = await _ticketService.UpdateTicketStatusAsync(ticketId, updateTicketStatusDto);
            return Ok(ApiResponse<TicketDetailDto>.Success(
                "Ticket status updated successfully",
                ticketDetailDto
            ));
        }
        [Authorize]
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
        [Authorize]
        [HttpGet("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> GetTicketMessageById(int ticketMessageId)
        {
            TicketMessageDto ticketMessageDto = await _ticketMessageService.GetTicketMessageByIdAsync(ticketMessageId);
            return Ok(ticketMessageDto);
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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