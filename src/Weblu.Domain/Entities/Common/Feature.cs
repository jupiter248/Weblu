using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Services;

namespace Weblu.Domain.Entities.Common
{
    public class Feature : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Service> Services { get; set; } = new List<Service>();
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        // Icon
    }
}