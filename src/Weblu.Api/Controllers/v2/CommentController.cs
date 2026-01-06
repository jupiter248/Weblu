using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.CommentDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers.v2
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
            PagedResponse<CommentDto> commentDtos = await _commentService.GetAllPagedCommentsAsync(commentParameters);
            return Ok(commentDtos);
        }
    }
}