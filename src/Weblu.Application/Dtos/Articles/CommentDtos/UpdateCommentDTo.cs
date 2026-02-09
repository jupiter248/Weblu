namespace Weblu.Application.DTOs.Articles.CommentDTOs
{
    public class UpdateCommentDTO
    {
        public string Text { get; set; } = default!;
        public int? ParentCommentId { get; set; }
    }
}