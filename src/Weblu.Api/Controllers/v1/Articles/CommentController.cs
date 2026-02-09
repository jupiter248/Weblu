using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.CommentDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Articles.Comments;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers.v1.Articles
{
    [ApiVersion("1")]
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
        public async Task<IActionResult> GetAll([FromQuery] CommentParameters commentParameters)
        {
            List<CommentDTO> commentDTOs = await _commentService.GetAllAsync(commentParameters);
            return Ok(commentDTOs);
        }
        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetById(int commentId)
        {
            CommentDTO commentDTOs = await _commentService.GetByIdAsync(commentId);
            return Ok(commentDTOs);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDTO createCommentDTO)
        {
            Validator.ValidateAndThrow(createCommentDTO, new CreateCommentValidator());

            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            CommentDTO commentDTOs = await _commentService.CreateAsync(userId, createCommentDTO);
            return CreatedAtAction(nameof(GetById), new { commentId = commentDTOs.Id }, ApiResponse<CommentDTO>.Success("Comment added successfully.", commentDTOs));
        }
        [Authorize]
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> Edit(int commentId, [FromBody] UpdateCommentDTO updateCommentDTO)
        {
            Validator.ValidateAndThrow(updateCommentDTO, new UpdateCommentValidator());

            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            CommentDTO commentDTOs = await _commentService.EditAsync(userId, commentId, updateCommentDTO);
            return Ok(ApiResponse<CommentDTO>.Success(
                "Comment updated successfully.",
                commentDTOs
            ));
        }
        [Authorize]
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _commentService.DeleteAsync(userId, commentId);
            return Ok(ApiResponse.Success("Comment deleted successfully."));
        }
    }
}