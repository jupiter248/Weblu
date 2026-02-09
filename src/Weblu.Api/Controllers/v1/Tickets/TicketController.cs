using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Tickets.TicketDTOs;
using Weblu.Application.DTOs.Tickets.TicketMessageDTOs;
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
            List<TicketSummaryDTO> ticketSummaryDTOs = await _ticketService.GetAllAsync(userId, ticketParameters);
            return Ok(ticketSummaryDTOs);
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
            TicketDetailDTO ticketDetailDTO = await _ticketService.GetByIdAsync(userId, ticketId);
            return Ok(ticketDetailDTO);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDTO createTicketDTO)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDTO ticketDetailDTO = await _ticketService.CreateAsync(userId, createTicketDTO);
            return CreatedAtAction(nameof(GetById), new { ticketId = ticketDetailDTO.Id }, ApiResponse<TicketDetailDTO>.Success(
                "Ticket created successfully",
                ticketDetailDTO
            ));
        }
        [Authorize]
        [HttpPut("{ticketId:int}")]
        public async Task<IActionResult> Edit(int ticketId, [FromBody] EditTicketDTO updateTicketDTO)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketDetailDTO ticketDetailDTO = await _ticketService.EditAsync(userId, ticketId, updateTicketDTO);
            return Ok(ApiResponse<TicketDetailDTO>.Success(
                "Ticket updated successfully",
                ticketDetailDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageTickets)]
        [HttpPut("{ticketId:int}/status")]
        public async Task<IActionResult> ChangeStatus(int ticketId, [FromBody] ChangeTicketStatusDTO changeTicketStatusDTO)
        {
            TicketDetailDTO ticketDetailDTO = await _ticketService.ChangeStatusAsync(ticketId, changeTicketStatusDTO);
            return Ok(ApiResponse<TicketDetailDTO>.Success(
                "Ticket status updated successfully",
                ticketDetailDTO
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
            TicketMessageDTO ticketMessageDTO = await _ticketMessageService.GetByIdAsync(ticketMessageId);
            return Ok(ticketMessageDTO);
        }
        [Authorize]
        [HttpPost("{ticketId:int}/reply")]
        public async Task<IActionResult> Reply(int ticketId, [FromBody] ReplyTicketDTO replyTicketDTO)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessageDTO ticketMessageDTO = await _ticketMessageService.ReplyAsync(userId, ticketId, replyTicketDTO);
            return CreatedAtAction(nameof(GetMessageById), new { ticketMessageId = ticketMessageDTO.Id }, ApiResponse<TicketMessageDTO>.Success(
                "Ticket replied successfully",
                ticketMessageDTO
            ));
        }
        [Authorize]
        [HttpPut("/message/{ticketMessageId:int}")]
        public async Task<IActionResult> EditMessage(int ticketMessageId, [FromBody] EditTicketMessageDTO editTicketMessageDTO)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            TicketMessageDTO ticketMessageDTO = await _ticketMessageService.EditAsync(userId, ticketMessageId, editTicketMessageDTO);
            return Ok(ApiResponse<TicketMessageDTO>.Success(
                "Ticket message updated successfully",
                ticketMessageDTO
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