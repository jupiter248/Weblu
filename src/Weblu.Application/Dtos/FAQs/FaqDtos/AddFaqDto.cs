namespace Weblu.Application.DTOs.FAQs.FAQDTOs
{
    public class CreateFAQDTO
    {
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}