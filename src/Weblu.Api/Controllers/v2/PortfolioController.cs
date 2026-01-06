using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers.v2
{
    [ApiController]
    [Route("api/portfolio")]
    [ApiVersion("2")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPortfolios([FromQuery] PortfolioParameters portfolioParameters)
        {
            PagedResponse<PortfolioSummaryDto> portfolioSummaryDtos = await _portfolioService.GetAllPagedPortfolioAsync(portfolioParameters);
            return Ok(portfolioSummaryDtos);
        }
    }
}