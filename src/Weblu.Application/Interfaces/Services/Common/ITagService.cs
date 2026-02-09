using Weblu.Application.Dtos.Common.TagDtos;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllAsync(TagParameters tagParameters);
        Task<TagDto> GetByIdAsync(int tagId);
        Task<TagDto> CreateAsync(CreateTagDto createTagDto);
        Task<TagDto> UpdateAsync(int tagId, UpdateTagDto updateTagDto);
        Task DeleteAsync(int tagId);
    }
}