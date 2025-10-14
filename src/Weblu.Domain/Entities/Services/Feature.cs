using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Services;

namespace Weblu.Domain.Entities.Services
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Service> Services { get; set; } = new List<Service>();
    }
}