using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Services.ServiceDTOs;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Parameters.Services;

namespace Weblu.Api.Controllers.v2.Services
{
    [ApiController]
    [Route("api/service")]
    [ApiVersion("2")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ServiceParameters serviceParameters)
        {
            PagedResponse<ServiceSummaryDTO> serviceSummaryDTOs = await _serviceService.GetAllPagedAsync(serviceParameters);
            return Ok(serviceSummaryDTOs);
        }
    }
}