namespace Weblu.Application.DTOs.About.SocialMediaDTOs
{
    public class SocialMediaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Link { get; set; }
        public string? IconUrl { get; set; }
        public string? IconAltText { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
    }
}