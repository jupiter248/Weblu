namespace Weblu.Application.DTOs.About.AboutUsDTOs
{
    public class CreateAboutUsDTO
    {
        public string Title { get; set; } = default!;
        public string SubTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Vision { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}