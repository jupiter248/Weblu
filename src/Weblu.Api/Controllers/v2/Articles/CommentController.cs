using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.CommentDTOs;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Api.Controllers.v2.Articles
{
    [ApiController]
    [Route("api/comment")]
    [ApiVersion("2")]
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
            PagedResponse<CommentDTO> commentDTOs = await _commentService.GetAllPagedAsync(commentParameters);
            return Ok(commentDTOs);
        }
    }
}