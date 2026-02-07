namespace Weblu.Application.Dtos.Services.ServiceDtos
{
    public class UpdateServiceDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public int BaseDurationInDays { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
    }
}