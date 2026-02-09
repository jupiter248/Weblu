using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.FAQs.FAQDtos;
using Weblu.Application.Interfaces.Services.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Application.Validations;
using Weblu.Application.Validations.FAQs.FAQ;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.FAQs
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/faq")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;
        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FAQParameters faqParameters)
        {
            List<FAQDto> faqDtos = await _faqService.GetAllAsync(faqParameters);
            return Ok(faqDtos);
        }
        [HttpGet("{faqId:int}")]
        public async Task<IActionResult> GetById(int faqId)
        {
            FAQDto faqDto = await _faqService.GetByIdAsync(faqId);
            return Ok(faqDto);
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFAQDto createFAQDto)
        {
            Validator.ValidateAndThrow(createFAQDto, new CreateFAQValidator());

            FAQDto faqDto = await _faqService.CreateAsync(createFAQDto);
            return CreatedAtAction(nameof(GetById), new { faqId = faqDto.Id }, ApiResponse<FAQDto>.Success("FAQ added successfully.", faqDto));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{faqId:int}")]
        public async Task<IActionResult> Update(int faqId, [FromBody] UpdateFAQDto updateFAQDto)
        {
            Validator.ValidateAndThrow(updateFAQDto, new UpdateFAQValidator());


            FAQDto faqDto = await _faqService.UpdateAsync(faqId, updateFAQDto);
            return Ok(ApiResponse<FAQDto>.Success(
                "FAQ updated successfully.",
                faqDto
            ));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpDelete("{faqId:int}")]
        public async Task<IActionResult> Delete(int faqId)
        {
            await _faqService.DeleteAsync(faqId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{articleId:int}/publish")]
        public async Task<IActionResult> Publish(int articleId)
        {
            await _faqService.Publish(articleId);
            return NoContent();
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{articleId:int}/unpublish")]
        public async Task<IActionResult> Unpublish(int articleId)
        {
            await _faqService.Unpublish(articleId);
            return NoContent();
        }
    }
}