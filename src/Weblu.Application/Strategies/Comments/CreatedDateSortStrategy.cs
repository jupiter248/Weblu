using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Comments
{
    public class CreatedDateSortStrategy : ICommentQueryStrategy
    {
        public IQueryable<Comment> Query(IQueryable<Comment> comments, CommentParameters commentParameters)
        {
            if (commentParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return comments.OrderByDescending(c => c.CreatedAt);
            }
            else if (commentParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return comments.OrderBy(c => c.CreatedAt);
            }
            else
            {
                return comments;
            }
        }
    }
}