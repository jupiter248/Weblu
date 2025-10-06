using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.FeatureDtos
{
    public class FeatureDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get;  set; }
    }
}