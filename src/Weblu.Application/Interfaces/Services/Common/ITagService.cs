using Weblu.Application.DTOs.Common.TagDTOs;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface ITagService
    {
        Task<List<TagDTO>> GetAllAsync(TagParameters tagParameters);
        Task<TagDTO> GetByIdAsync(int tagId);
        Task<TagDTO> CreateAsync(CreateTagDTO createTagDTO);
        Task<TagDTO> UpdateAsync(int tagId, UpdateTagDTO updateTagDTO);
        Task DeleteAsync(int tagId);
    }
}