using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Comments;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Strategies.Comments
{
    public class FilterByArticleIdStrategy : ICommentQueryStrategy
    {
        public IQueryable<Comment> Query(IQueryable<Comment> comments, CommentParameters commentParameters)
        {

            return comments.Where(c => c.ArticleId == commentParameters.ArticleId);

        }
    }
}