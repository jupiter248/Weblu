namespace Weblu.Application.DTOs.Articles.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string Text { get; set; } = default!;
        public int? ParentCommentId { get; set; }
        public int ArticleId { get; set; }
    }
}