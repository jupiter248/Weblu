using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Faqs;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/faq")]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;
        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFaqs([FromQuery] FaqParameters faqParameters)
        {
            List<FaqDto> faqDtos = await _faqService.GetAllFaqsAsync(faqParameters);
            return Ok(faqDtos);
        }
        [HttpGet("{faqId:int}")]
        public async Task<IActionResult> GetFaqById(int faqId)
        {
            FaqDto faqDto = await _faqService.GetFaqByIdAsync(faqId);
            return Ok(faqDto);
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPost]
        public async Task<IActionResult> AddFaq([FromBody] AddFaqDto addFaqDto)
        {
            Validator.ValidateAndThrow(addFaqDto, new AddFaqValidator());

            FaqDto faqDto = await _faqService.AddFaqAsync(addFaqDto);
            return CreatedAtAction(nameof(GetFaqById), new { faqId = faqDto.Id }, ApiResponse<FaqDto>.Success("Faq added successfully.", faqDto));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{faqId:int}")]
        public async Task<IActionResult> UpdateFaq(int faqId, [FromBody] UpdateFaqDto updateFaqDto)
        {
            Validator.ValidateAndThrow(updateFaqDto, new UpdateFaqValidator());


            FaqDto faqDto = await _faqService.UpdateFaqAsync(faqId, updateFaqDto);
            return Ok(ApiResponse<FaqDto>.Success(
                "Faq updated successfully.",
                faqDto
            ));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpDelete("{faqId:int}")]
        public async Task<IActionResult> DeleteFaq(int faqId)
        {
            await _faqService.DeleteFaqAsync(faqId);
            return NoContent();
        }
    }
}