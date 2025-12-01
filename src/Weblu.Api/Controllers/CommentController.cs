using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.CommentDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Comments;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] CommentParameters commentParameters)
        {
            List<CommentDto> commentDtos = await _commentService.GetAllCommentsAsync(commentParameters);
            return Ok(commentDtos);
        }
        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            CommentDto commentDtos = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(commentDtos);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDto addCommentDto)
        {
            Validator.ValidateAndThrow(addCommentDto, new AddCommentValidator());

            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            CommentDto commentDtos = await _commentService.AddCommentAsync(userId, addCommentDto);
            return CreatedAtAction(nameof(GetCommentById), new { commentId = commentDtos.Id }, ApiResponse<CommentDto>.Success("Comment added successfully.", commentDtos));
        }
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentDTo updateCommentDTo)
        {
            Validator.ValidateAndThrow(updateCommentDTo, new UpdateCommentValidator());

            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            CommentDto commentDtos = await _commentService.UpdateCommentAsync(userId, commentId, updateCommentDTo);
            return Ok(ApiResponse<CommentDto>.Success(
                "Comment updated successfully.",
                commentDtos
            ));
        }
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _commentService.DeleteCommentAsync(userId, commentId);
            return Ok(ApiResponse.Success("Comment deleted successfully."));
        }
    }
}