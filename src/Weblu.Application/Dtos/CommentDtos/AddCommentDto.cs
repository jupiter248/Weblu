using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.CommentDtos
{
    public class AddCommentDto
    {
        public string Text { get; set; } = default!;
        public int? ParentCommentId { get; set; }
        public int ArticleId { get; set;}
    }
}