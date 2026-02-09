using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Common.SearchDTOs;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;

namespace Weblu.Api.Controllers.v1.Common
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
        public async Task<IActionResult> Search([FromQuery] string text, [FromQuery] SearchParameters searchParameters)
        {
            PagedResponse<SearchItemDTO> searchItemDTOs = await _searchService.SearchAsync(text, searchParameters);
            return Ok(searchItemDTOs);
        }
    }
}