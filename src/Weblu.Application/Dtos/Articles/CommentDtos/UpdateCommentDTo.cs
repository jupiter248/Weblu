namespace Weblu.Application.Dtos.Articles.CommentDtos
{
    public class UpdateCommentDto
    {
        public string Text { get; set; } = default!;
        public int? ParentCommentId { get; set; }
    }
}