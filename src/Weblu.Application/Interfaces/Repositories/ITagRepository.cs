using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<IReadOnlyList<Tag>> GetAllTagsAsync(TagParameters tagParameters);
        Task<Tag?> GetTagByIdAsync(int tagId);
        Task AddTagAsync(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(Tag tag);
    }
}