using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.FAQs.FAQCategoryDtos;
using Weblu.Application.Interfaces.Services.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Application.Validations;
using Weblu.Application.Validations.FAQs.FAQCategory;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.FAQs
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/faq-category")]
    public class FAQCategoryController : ControllerBase
    {
        private readonly IFAQCategoryService _faqCategoryService;
        public FAQCategoryController(IFAQCategoryService faqCategoryService)
        {
            _faqCategoryService = faqCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FAQCategoryParameters faqCategoryParameters)
        {
            List<FAQCategoryDto> faqCategoryDtos = await _faqCategoryService.GetAllAsync(faqCategoryParameters);
            return Ok(faqCategoryDtos);
        }
        [HttpGet("{faqCategoryId:int}")]
        public async Task<IActionResult> GetById(int faqCategoryId)
        {
            FAQCategoryDto faqCategoryDto = await _faqCategoryService.GetByIdAsync(faqCategoryId);
            return Ok(faqCategoryDto);
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFAQCategoryDto createFAQCategoryDto)
        {
            Validator.ValidateAndThrow(createFAQCategoryDto, new CreateFAQCategoryValidator());

            FAQCategoryDto faqCategoryDto = await _faqCategoryService.CreateAsync(createFAQCategoryDto);
            return CreatedAtAction(nameof(GetById), new { faqCategoryId = faqCategoryDto.Id }, ApiResponse<FAQCategoryDto>.Success("FAQ category added successfully.", faqCategoryDto));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{faqCategoryId:int}")]
        public async Task<IActionResult> Update(int faqCategoryId, [FromBody] UpdateFAQCategoryDto updateFAQCategoryDto)
        {
            Validator.ValidateAndThrow(updateFAQCategoryDto, new UpdateFAQCategoryValidator());

            FAQCategoryDto faqCategoryDto = await _faqCategoryService.UpdateAsync(faqCategoryId, updateFAQCategoryDto);
            return Ok(ApiResponse<FAQCategoryDto>.Success(
                "FAQ category updated successfully.",
                faqCategoryDto
            ));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpDelete("{faqCategoryId:int}")]
        public async Task<IActionResult> Delete(int faqCategoryId)
        {
            await _faqCategoryService.DeleteAsync(faqCategoryId);
            return NoContent();
        }
    }
}