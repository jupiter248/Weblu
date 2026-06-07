using Weblu.Application.Common.Parameters;

namespace Weblu.Application.Parameters.Articles
{
    public class CommentParameters : BaseParameters
    {
        public required int ArticleId { get; set; }
        public string? UserId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}