using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Services
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public string Description { get; set; } = default!;
        public required string ShortDescription { get; set; }
        public int BaseDurationInDays { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? ActivatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Feature> Features { get; set; } = new List<Feature>();
        public List<Method> Methods { get; set; } = new List<Method>();
        public List<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();
    }
}