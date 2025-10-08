using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.FeatureDtos;

namespace Weblu.Application.Dtos.ServiceDtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public int BaseDurationInDays { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public string? ActivatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public List<FeatureDto>? Features { get; set; }
    }
}