using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public bool IsEdited { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
        public int? ParentCommentId { get; set; }
        public int ArticleId { get; set; } = default!;
        public CommentUserDto User { get; set; } = default!;
    }
}