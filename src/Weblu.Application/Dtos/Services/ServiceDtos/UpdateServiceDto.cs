namespace Weblu.Application.DTOs.Services.ServiceDTOs
{
    public class UpdateServiceDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public int BaseDurationInDays { get; set; }
        public decimal BasePrice { get; set; }
    }
}