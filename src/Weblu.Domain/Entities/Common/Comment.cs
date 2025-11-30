using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Domain.Entities.Common
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public bool IsEdited { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public int? ParentCommentId { get; set; }
        public string UserId { get; set; } = default!;
        public int ArticleId { get; set; } = default!;
        public Article Article { get; set; } = default!;

    }
}