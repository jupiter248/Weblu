using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.CommentDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllCommentsAsync(CommentParameters commentParameters);
        Task<PagedResponse<CommentDto>> GetAllPagedCommentsAsync(CommentParameters commentParameters);
        Task<CommentDto> GetCommentByIdAsync(int commentId);
        Task<CommentDto> AddCommentAsync(string userId, AddCommentDto addCommentDto);
        Task<CommentDto> UpdateCommentAsync(string userId, int commentId, UpdateCommentDTo updateCommentDTo);
        Task DeleteCommentAsync(string userId, int commentId);
    }
}