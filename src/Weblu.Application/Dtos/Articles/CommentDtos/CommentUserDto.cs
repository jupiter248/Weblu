namespace Weblu.Application.Dtos.Articles.CommentDtos
{
    public class CommentUserDto
    {
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string? UserProfileUrl { get; set; }
        public string? UserProfileAltText { get; set; }
    }
}