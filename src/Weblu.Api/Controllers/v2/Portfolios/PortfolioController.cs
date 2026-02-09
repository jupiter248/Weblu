using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters.Portfolios;

namespace Weblu.Api.Controllers.v2.Portfolios
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
        public async Task<IActionResult> GetAll([FromQuery] PortfolioParameters portfolioParameters)
        {
            PagedResponse<PortfolioSummaryDTO> portfolioSummaryDTOs = await _portfolioService.GetAllPagedAsync(portfolioParameters);
            return Ok(portfolioSummaryDTOs);
        }
    }
}