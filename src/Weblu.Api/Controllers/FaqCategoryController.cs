using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.FaqCategoryDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.FaqCategory;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/faq-category")]
    public class FaqCategoryController : ControllerBase
    {
        private readonly IFaqCategoryService _faqCategoryService;
        public FaqCategoryController(IFaqCategoryService faqCategoryService)
        {
            _faqCategoryService = faqCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFaqCategories()
        {
            List<FaqCategoryDto> faqCategoryDtos = await _faqCategoryService.GetAllFaqCategoriesAsync();
            return Ok(faqCategoryDtos);
        }
        [HttpGet("{faqCategoryId:int}")]
        public async Task<IActionResult> GetFaqCategoryById(int faqCategoryId)
        {
            FaqCategoryDto faqCategoryDto = await _faqCategoryService.GetFaqCategoryByIdAsync(faqCategoryId);
            return Ok(faqCategoryDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFaqCategory([FromBody] AddFaqCategoryDto addFaqCategoryDto)
        {
            Validator.ValidateAndThrow(addFaqCategoryDto, new AddFaqCategoryValidator());

            FaqCategoryDto faqCategoryDto = await _faqCategoryService.AddFaqCategoryAsync(addFaqCategoryDto);
            return CreatedAtAction(nameof(GetFaqCategoryById), new { faqCategoryId = faqCategoryDto.Id }, ApiResponse<FaqCategoryDto>.Success("Faq category added successfully.", faqCategoryDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{faqCategoryId:int}")]
        public async Task<IActionResult> UpdateFaqCategory(int faqCategoryId, [FromBody] UpdateFaqCategoryDto updateFaqCategoryDto)
        {
            Validator.ValidateAndThrow(updateFaqCategoryDto, new UpdateFaqCategoryValidator());

            FaqCategoryDto faqCategoryDto = await _faqCategoryService.UpdateFaqCategoryAsync(faqCategoryId, updateFaqCategoryDto);
            return Ok(ApiResponse<FaqCategoryDto>.Success(
                "Faq category updated successfully.",
                faqCategoryDto
            ));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{faqCategoryId:int}")]
        public async Task<IActionResult> DeleteFaqCategory(int faqCategoryId)
        {
            await _faqCategoryService.DeleteFaqCategoryAsync(faqCategoryId);
            return NoContent();
        }
    }
}