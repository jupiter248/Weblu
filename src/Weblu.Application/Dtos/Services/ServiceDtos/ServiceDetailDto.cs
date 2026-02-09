using Weblu.Application.Dtos.Services.ServiceDtos.ServiceImageDtos;

namespace Weblu.Application.Dtos.Services.ServiceDtos
{
    public class ServiceDetailDto
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
        public List<ServiceImageDto>? Images { get; set; }


    }
}