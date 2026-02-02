using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.SearchDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromRoute] string text, [FromQuery] SearchParameters searchParameters)
        {
            PagedResponse<SearchItemDto> searchItemDtos = await _searchService.SearchAsync(text, searchParameters);
            return Ok(searchItemDtos);
        }
    }
}