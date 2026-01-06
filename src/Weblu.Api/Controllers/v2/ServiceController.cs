using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers.v2
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
        public async Task<IActionResult> GetAllServices([FromQuery] ServiceParameters serviceParameters)
        {
            PagedResponse<ServiceSummaryDto> serviceSummaryDtos = await _serviceService.GetAllPagedServiceAsync(serviceParameters);
            return Ok(serviceSummaryDtos);
        }
    }
}