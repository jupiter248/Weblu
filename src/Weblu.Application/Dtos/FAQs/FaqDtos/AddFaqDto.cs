namespace Weblu.Application.Dtos.FAQs.FAQDtos
{
    public class CreateFAQDto
    {
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}