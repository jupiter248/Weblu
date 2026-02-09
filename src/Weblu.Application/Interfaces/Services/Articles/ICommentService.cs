using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.CommentDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllAsync(CommentParameters commentParameters);
        Task<PagedResponse<CommentDto>> GetAllPagedAsync(CommentParameters commentParameters);
        Task<CommentDto> GetByIdAsync(int commentId);
        Task<CommentDto> CreateAsync(string userId, CreateCommentDto createCommentDto);
        Task<CommentDto> EditAsync(string userId, int commentId, UpdateCommentDto updateCommentDto);
        Task DeleteAsync(string userId, int commentId);
    }
}