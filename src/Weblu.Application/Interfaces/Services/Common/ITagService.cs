using Weblu.Application.Dtos.Common.TagDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllTagsAsync(TagParameters tagParameters);
        Task<TagDto> GetTagByIdAsync(int tagId);
        Task<TagDto> AddTagAsync(AddTagDto addTagDto);
        Task<TagDto> UpdateTagAsync(int tagId, UpdateTagDto updateTagDto);
        Task DeleteTagAsync(int tagId);
    }
}