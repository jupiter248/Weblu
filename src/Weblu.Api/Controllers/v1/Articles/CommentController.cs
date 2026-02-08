using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.CommentDtos;
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
            List<CommentDto> commentDtos = await _commentService.GetAllAsync(commentParameters);
            return Ok(commentDtos);
        }
        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetById(int commentId)
        {
            CommentDto commentDtos = await _commentService.GetByIdAsync(commentId);
            return Ok(commentDtos);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            Validator.ValidateAndThrow(createCommentDto, new CreateCommentValidator());

            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            CommentDto commentDtos = await _commentService.CreateAsync(userId, createCommentDto);
            return CreatedAtAction(nameof(GetById), new { commentId = commentDtos.Id }, ApiResponse<CommentDto>.Success("Comment added successfully.", commentDtos));
        }
        [Authorize]
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> Edit(int commentId, [FromBody] UpdateCommentDto updateCommentDto)
        {
            Validator.ValidateAndThrow(updateCommentDto, new UpdateCommentValidator());

            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            CommentDto commentDtos = await _commentService.EditAsync(userId, commentId, updateCommentDto);
            return Ok(ApiResponse<CommentDto>.Success(
                "Comment updated successfully.",
                commentDtos
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