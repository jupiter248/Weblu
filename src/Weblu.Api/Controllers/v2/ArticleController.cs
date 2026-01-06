using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers.v2
{
    // This is just a test controller for version 2
    [ApiController]
    [Route("api/article")]
    [ApiVersion("2")]

    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(
            IArticleService articleService)
        {
            _articleService = articleService;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllArticles([FromQuery] ArticleParameters articleParameters)
        {
            PagedResponse<ArticleSummaryDto> articleSummaryDtos = await _articleService.GetAllPagedArticlesAsync(articleParameters);
            return Ok(articleSummaryDtos);
        }
    }
}