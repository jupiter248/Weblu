using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.FAQs.FAQCategoryDTOs;
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
            List<FAQCategoryDTO> faqCategoryDTOs = await _faqCategoryService.GetAllAsync(faqCategoryParameters);
            return Ok(faqCategoryDTOs);
        }
        [HttpGet("{faqCategoryId:int}")]
        public async Task<IActionResult> GetById(int faqCategoryId)
        {
            FAQCategoryDTO faqCategoryDTO = await _faqCategoryService.GetByIdAsync(faqCategoryId);
            return Ok(faqCategoryDTO);
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFAQCategoryDTO createFAQCategoryDTO)
        {
            Validator.ValidateAndThrow(createFAQCategoryDTO, new CreateFAQCategoryValidator());

            FAQCategoryDTO faqCategoryDTO = await _faqCategoryService.CreateAsync(createFAQCategoryDTO);
            return CreatedAtAction(nameof(GetById), new { faqCategoryId = faqCategoryDTO.Id }, ApiResponse<FAQCategoryDTO>.Success("FAQ category added successfully.", faqCategoryDTO));
        }
        [Authorize(Policy = Permissions.ManageFAQs)]
        [HttpPut("{faqCategoryId:int}")]
        public async Task<IActionResult> Update(int faqCategoryId, [FromBody] UpdateFAQCategoryDTO updateFAQCategoryDTO)
        {
            Validator.ValidateAndThrow(updateFAQCategoryDTO, new UpdateFAQCategoryValidator());

            FAQCategoryDTO faqCategoryDTO = await _faqCategoryService.UpdateAsync(faqCategoryId, updateFAQCategoryDTO);
            return Ok(ApiResponse<FAQCategoryDTO>.Success(
                "FAQ category updated successfully.",
                faqCategoryDTO
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