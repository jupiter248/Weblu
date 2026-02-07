namespace Weblu.Application.Dtos.FAQs.FaqDtos
{
    public class AddFaqDto
    {
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}