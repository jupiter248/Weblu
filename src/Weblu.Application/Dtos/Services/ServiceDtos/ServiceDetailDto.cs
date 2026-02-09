using Weblu.Application.DTOs.Services.ServiceDTOs.ServiceImageDTOs;

namespace Weblu.Application.DTOs.Services.ServiceDTOs
{
    public class ServiceDetailDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public int BaseDurationInDays { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsPublished { get; set; }
        public string? PublishedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public List<ServiceImageDTO>? Images { get; set; }


    }
}