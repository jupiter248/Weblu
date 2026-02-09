using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.CommentDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface ICommentService
    {
        Task<List<CommentDTO>> GetAllAsync(CommentParameters commentParameters);
        Task<PagedResponse<CommentDTO>> GetAllPagedAsync(CommentParameters commentParameters);
        Task<CommentDTO> GetByIdAsync(int commentId);
        Task<CommentDTO> CreateAsync(string userId, CreateCommentDTO createCommentDTO);
        Task<CommentDTO> EditAsync(string userId, int commentId, UpdateCommentDTO updateCommentDTO);
        Task DeleteAsync(string userId, int commentId);
    }
}