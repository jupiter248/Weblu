using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Strategies.Comments
{
    public class CommentQueryHandler
    {
        private readonly ICommentQueryStrategy _commentQueryStrategy;
        public CommentQueryHandler(ICommentQueryStrategy commentQueryStrategy)
        {
            _commentQueryStrategy = commentQueryStrategy;
        }

        public IQueryable<Comment> ExecuteCommentQuery(IQueryable<Comment> comments, CommentParameters commentParameters)
        {
            return _commentQueryStrategy.Query(comments, commentParameters);
        }
    }
}