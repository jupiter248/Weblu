using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Media
{
    public abstract class Media : BaseEntity
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public string? AltText { get; set; }
        public DateTimeOffset AddedAt { get; private set; } = DateTimeOffset.Now;
    }
}