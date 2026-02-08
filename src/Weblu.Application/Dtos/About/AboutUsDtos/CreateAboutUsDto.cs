namespace Weblu.Application.Dtos.About.AboutUsDtos
{
    public class CreateAboutUsDto
    {
        public string Title { get; set; } = default!;
        public string SubTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Vision { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}