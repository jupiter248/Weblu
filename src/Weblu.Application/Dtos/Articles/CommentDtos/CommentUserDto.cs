namespace Weblu.Application.DTOs.Articles.CommentDTOs
{
    public class CommentUserDTO
    {
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string? UserProfileUrl { get; set; }
        public string? UserProfileAltText { get; set; }
    }
}