using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.TagDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
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