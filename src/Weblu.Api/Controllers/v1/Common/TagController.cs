using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Common.TagDTOs;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Common.Tags;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Common
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/tag")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TagParameters tagParameters)
        {
            List<TagDTO> tagDTOs = await _tagService.GetAllAsync(tagParameters);
            return Ok(tagDTOs);
        }
        [HttpGet("{tagId:int}")]
        public async Task<IActionResult> GetById(int tagId)
        {
            TagDTO tagDTO = await _tagService.GetByIdAsync(tagId);
            return Ok(tagDTO);
        }
        [Authorize(Policy = Permissions.ManageTags)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagDTO createTagDTO)
        {
            Validator.ValidateAndThrow(createTagDTO, new CreateTagValidator());
            TagDTO tagDTO = await _tagService.CreateAsync(createTagDTO);
            return CreatedAtAction(nameof(GetById), new { tagId = tagDTO.Id }, ApiResponse<TagDTO>.Success
            (
                "Tag created successfully.",
                tagDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageTags)]
        [HttpPut("{tagId:int}")]
        public async Task<IActionResult> Update(int tagId, [FromBody] UpdateTagDTO updateTagDTO)
        {
            Validator.ValidateAndThrow(updateTagDTO, new UpdateTagValidator());
            TagDTO tagDTO = await _tagService.UpdateAsync(tagId, updateTagDTO);
            return Ok(ApiResponse<TagDTO>.Success
            (
                "Tag updated successfully.",
                tagDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageTags)]
        [HttpDelete("{tagId:int}")]
        public async Task<IActionResult> Delete(int tagId)
        {
            await _tagService.DeleteAsync(tagId);
            return Ok(ApiResponse.Success
            (
                "Tag deleted successfully."
            ));
        }
    }
}