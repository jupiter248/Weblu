namespace Weblu.Application.Dtos.Articles.CommentDtos
{
    public class AddCommentDto
    {
        public string Text { get; set; } = default!;
        public int? ParentCommentId { get; set; }
        public int ArticleId { get; set;}
    }
}