using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Common.TagDtos;
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
        public async Task<IActionResult> GetAllTags([FromQuery] TagParameters tagParameters)
        {
            List<TagDto> tagDtos = await _tagService.GetAllTagsAsync(tagParameters);
            return Ok(tagDtos);
        }
        [HttpGet("{tagId:int}")]
        public async Task<IActionResult> GetTagById(int tagId)
        {
            TagDto tagDto = await _tagService.GetTagByIdAsync(tagId);
            return Ok(tagDto);
        }
        [Authorize(Policy = Permissions.ManageTags)]
        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] AddTagDto addTagDto)
        {
            Validator.ValidateAndThrow(addTagDto, new AddTagValidator());
            TagDto tagDto = await _tagService.AddTagAsync(addTagDto);
            return CreatedAtAction(nameof(GetTagById), new { tagId = tagDto.Id }, ApiResponse<TagDto>.Success
            (
                "Tag created successfully.",
                tagDto
            ));
        }
        [Authorize(Policy = Permissions.ManageTags)]
        [HttpPut("{tagId:int}")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] UpdateTagDto updateTagDto)
        {
            Validator.ValidateAndThrow(updateTagDto, new UpdateTagValidator());
            TagDto tagDto = await _tagService.UpdateTagAsync(tagId, updateTagDto);
            return Ok(ApiResponse<TagDto>.Success
            (
                "Tag updated successfully.",
                tagDto
            ));
        }
        [Authorize(Policy = Permissions.ManageTags)]
        [HttpDelete("{tagId:int}")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            await _tagService.DeleteTagAsync(tagId);
            return Ok(ApiResponse.Success
            (
                "Tag deleted successfully."
            ));
        }
    }
}