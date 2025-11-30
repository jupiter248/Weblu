using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IReadOnlyList<Comment>> GetAllCommentsAsync(CommentParameters commentParameters);
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task AddCommentAsync(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}