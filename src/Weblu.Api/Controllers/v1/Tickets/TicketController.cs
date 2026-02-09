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
        public async Task<IActionResult> GetAll([FromQuery] TicketParameters ticketParameters)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            List<TicketSummaryDto> ticketSummaryDtos = await _ticketService.GetAllAsync(userId, ticketParameters);
            return Ok(ticketSummaryDtos);
        }
        [Authorize]
        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetById(int ticketId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDto ticketDetailDto = await _ticketService.GetByIdAsync(userId, ticketId);
            return Ok(ticketDetailDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto createTicketDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDto ticketDetailDto = await _ticketService.CreateAsync(userId, createTicketDto);
            return CreatedAtAction(nameof(GetById), new { ticketId = ticketDetailDto.Id }, ApiResponse<TicketDetailDto>.Success(
                "Ticket created successfully",
                ticketDetailDto
            ));
        }
        [Authorize]
        [HttpPut("{ticketId:int}")]
        public async Task<IActionResult> Edit(int ticketId, [FromBody] EditTicketDto updateTicketDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDto ticketDetailDto = await _ticketService.EditAsync(userId, ticketId, updateTicketDto);
            return Ok(ApiResponse<TicketDetailDto>.Success(
                "Ticket updated successfully",
                ticketDetailDto
            ));
        }
        [Authorize(Policy = Permissions.ManageTickets)]
        [HttpPut("{ticketId:int}/status")]
        public async Task<IActionResult> ChangeStatus(int ticketId, [FromBody] ChangeTicketStatusDto changeTicketStatusDto)
        {
            TicketDetailDto ticketDetailDto = await _ticketService.ChangeStatusAsync(ticketId, changeTicketStatusDto);
            return Ok(ApiResponse<TicketDetailDto>.Success(
                "Ticket status updated successfully",
                ticketDetailDto
            ));
        }
        [Authorize]
        [HttpDelete("{ticketId:int}")]
        public async Task<IActionResult> Delete(int ticketId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _ticketService.DeleteAsync(userId, ticketId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> GetMessageById(int ticketMessageId)
        {
            TicketMessageDto ticketMessageDto = await _ticketMessageService.GetByIdAsync(ticketMessageId);
            return Ok(ticketMessageDto);
        }
        [Authorize]
        [HttpPost("{ticketId:int}/reply")]
        public async Task<IActionResult> Reply(int ticketId, [FromBody] ReplyTicketDto replyTicketDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessageDto ticketMessageDto = await _ticketMessageService.ReplyAsync(userId, ticketId, replyTicketDto);
            return CreatedAtAction(nameof(GetMessageById), new { ticketMessageId = ticketMessageDto.Id }, ApiResponse<TicketMessageDto>.Success(
                "Ticket replied successfully",
                ticketMessageDto
            ));
        }
        [Authorize]
        [HttpPut("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> EditMessage(int ticketMessageId, [FromBody] EditTicketMessageDto editTicketMessageDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessageDto ticketMessageDto = await _ticketMessageService.EditAsync(userId, ticketMessageId, editTicketMessageDto);
            return Ok(ApiResponse<TicketMessageDto>.Success(
                "Ticket message updated successfully",
                ticketMessageDto
            ));
        }
        [Authorize]
        [HttpDelete("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> DeleteMessage(int ticketMessageId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _ticketMessageService.DeleteAsync(userId, ticketMessageId);
            return NoContent();
        }
    }
}