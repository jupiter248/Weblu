using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Parameters
{
    public class CommentParameters
    {
        public int? ArticleId { get; set; }
        public string? UserId { get; set; }
        public CreatedDateSort CreatedDateSort { get; set; }
        public int? ParentCommentId { get; set; }
    }
}