using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.FAQs.FAQDTOs;
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
            List<FAQDTO> faqDTOs = await _faqService.GetAllAsync(faqParameters);
            return Ok(faqDTOs);
        }
        [HttpGet("{faqId:int}")]
        public async Task<IActionResult> GetById(int faqId)
        {
            FAQDTO faqDTO = await _faqService.GetByIdAsync(faqId);
            return Ok(faqDTO);
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFAQDTO createFAQDTO)
        {
            Validator.ValidateAndThrow(createFAQDTO, new CreateFAQValidator());

            FAQDTO faqDTO = await _faqService.CreateAsync(createFAQDTO);
            return CreatedAtAction(nameof(GetById), new { faqId = faqDTO.Id }, ApiResponse<FAQDTO>.Success("FAQ added successfully.", faqDTO));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{faqId:int}")]
        public async Task<IActionResult> Update(int faqId, [FromBody] UpdateFAQDTO updateFAQDTO)
        {
            Validator.ValidateAndThrow(updateFAQDTO, new UpdateFAQValidator());


            FAQDTO faqDTO = await _faqService.UpdateAsync(faqId, updateFAQDTO);
            return Ok(ApiResponse<FAQDTO>.Success(
                "FAQ updated successfully.",
                faqDTO
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